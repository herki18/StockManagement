import { Fruit } from './../_models/fruit';
import { Variety } from './../_models/variety';
import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs/Rx';

import { UserService, AuthenticationService } from '@/_services';
import { StocksService } from '@/_services';
import { Stock } from '@/_models/stock';
import { User } from '@/_models';

@Component({ templateUrl: 'stocks.component.html',
styles: [`
        :host ::ng-deep .ui-table .ui-table-thead > tr > th {
            position: -webkit-sticky;
            position: sticky;
            top: 55px;
        }

        @media screen and (max-width: 64em) {
            :host ::ng-deep .ui-table .ui-table-thead > tr > th {
                top: 100px;
            }
        }
`] })
export class StocksComponent implements OnInit {
    currentUser: User;
    userFromApi: User;

    varieties: Variety[];
    settings: any;

    displayDialog: boolean;
    stock: Stock = { id: 0, fruit: '', fruitId: 0, variety: '', varietyId: 0, quantity: 0 }; //  = { };
    selectedStock: Stock;
    stocks: Stock[];
    newStock: boolean;
    cols: any[];

    constructor(
        private userService: UserService,
        private stocksService: StocksService,
        private authenticationService: AuthenticationService
    ) {
        this.currentUser = this.authenticationService.currentUserValue;
    }

    ngOnInit() {
        this.userService.getById(this.currentUser.id).pipe(first()).subscribe(user => {
            this.userFromApi = user;
        });

        Observable.forkJoin(
            this.stocksService.getAll(),
        ).subscribe(data => {
            this.stocks = data[0];
        });

        this.cols = [
            { field: 'id', header: 'StockId' },
            { field: 'fruit', header: 'Fruit'},
            { field: 'variety', header: 'Variety'},
            { field: 'quantity', header: 'Quantity' }
        ];
    }
}
