import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Batch } from '@/_models/';
import { environment } from 'src/environments/environment.prod';

@Injectable({ providedIn: 'root' })
export class BatchesService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Batch[]>(`${environment.api}batches`);
    }

    getById(id: number) {
        return this.http.get<Batch>(`${environment.api}batches/${id}`);
    }

    createBatch(batch: Batch) {
        return this.http.post<Batch>(`${environment.api}batches`, batch, {observe: 'response'});
    }

    updateBatch(batch: Batch) {
        return this.http.put<Batch>(`${environment.api}batches/${batch.id}`, batch, {observe: 'response'});
    }

    deleteBatch(id: number) {
        return this.http.delete(`${environment.api}batches/${id}`);
    }
}
