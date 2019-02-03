import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Stock } from '@/_models/';
import { environment } from 'src/environments/environment.prod';

@Injectable({ providedIn: 'root' })
export class StocksService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Stock[]>(`${environment.api}stocks`);
    }

    getById(id: number) {
        return this.http.get<Stock>(`${environment.api}stocks/${id}`);
    }
}