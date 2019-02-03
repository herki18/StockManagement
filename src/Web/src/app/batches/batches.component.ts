import { Observable } from 'rxjs/Rx';
import {SelectItem} from 'primeng/api';

import { Batch } from './../_models/batch';
import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@/_models';
import { UserService, AuthenticationService } from '@/_services';

import { FruitsService } from '@/_services';
import { BatchesService } from '@/_services';

import { Fruit } from '@/_models';

@Component({
    templateUrl: 'batches.component.html' })
export class BatchesComponent implements OnInit {
    currentUser: User;
    userFromApi: User;

    fruits: Fruit[];
    displayFruits: SelectItem[];

    settings: any;

    displayDialog: boolean;
    batch: Batch = { id: 0, fruit: '', fruitId: 0, variety: '', varietyId: 0, quantity: 0 };
    selectedBatch: Batch;
    batches: Batch[];
    newBatch: boolean;
    cols: any[];

    constructor(
        private userService: UserService,
        private authenticationService: AuthenticationService,
        private batchesService: BatchesService,
        private fruitsService: FruitsService
    ) {
        this.currentUser = this.authenticationService.currentUserValue;
        this.displayFruits = new Array();
    }

    showDialogToAdd() {
        this.newBatch = true;
        this.batch = { id: 0, fruit: '', fruitId: 0, variety: '', varietyId: 0, quantity: 0 };
        this.displayDialog = true;
    }

    save() {
        const batches = [...this.batches];
        if (this.newBatch) {
            this.batchesService.createBatch(this.batch).subscribe(data => {
                batches.push(data.body);
            });
        } else {
            this.batchesService.updateBatch(this.batch).subscribe(data => {
                batches[this.batches.indexOf(this.selectedBatch)] = data.body;
            });
        }

        this.batches = batches;
        this.batch = null;
        this.displayDialog = false;
    }

    delete() {
        const index = this.batches.indexOf(this.selectedBatch);
        this.batchesService.deleteBatch(this.selectedBatch.id).subscribe();
        this.batches = this.batches.filter((val, i) => i !== index);
        this.batch = null;
        this.displayDialog = false;
    }

    onRowSelect(event) {
        this.newBatch = false;
        this.batch = this.cloneStock(event.data);
        this.displayDialog = true;
    }

    cloneStock(s: Batch): Batch {
        const batch = { id: 0, fruit: '', fruitId: 0, variety: '', varietyId: 0, quantity: 0 };
        // tslint:disable-next-line:forin
        for (const prop in s) {
            batch[prop] = s[prop];
        }
        return batch;
    }

    ngOnInit() {
        this.userService.getById(this.currentUser.id).pipe(first()).subscribe(user => {
            this.userFromApi = user;
        });

        Observable.forkJoin(
            this.batchesService.getAll(),
            this.fruitsService.getAll()
        ).subscribe(data => {
            this.batches = data[0];
            this.fruits = data[1];

            this.fruits.forEach(f =>
                this.displayFruits.push(
                  { label: f.name + ' ' + f.variety.name, value: f.id })
            );
        });

        this.cols = [
            { field: 'id', header: 'StockId' },
            { field: 'fruit', header: 'Fruit'},
            { field: 'variety', header: 'Variety'},
            { field: 'quantity', header: 'Quantity' }
        ];
    }
}
