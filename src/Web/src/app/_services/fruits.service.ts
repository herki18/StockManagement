import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Fruit } from '@/_models/';
import { environment } from 'src/environments/environment.prod';

@Injectable({ providedIn: 'root' })
export class FruitsService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Fruit[]>(`${environment.api}fruits`);
    }
}
