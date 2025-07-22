import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from './products.model';
import { ApiEnvelope } from './products.model';
import { Observable, map } from 'rxjs';

const BASE_URL = 'https://localhost:7181/api/Products';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private http = inject(HttpClient);

    getProducts(): Observable<Product[]> {
    return this.http
        .get<ApiEnvelope<{ data: Product[] }>>(`${BASE_URL}/ListAll`)
        .pipe(map(res => res.data.data));
    }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(BASE_URL, product);
  }

  updateProduct(id: string, product: Product): Observable<Product> {
    return this.http.put<Product>(`${BASE_URL}/${id}`, product);
  }

  deleteProduct(id: string): Observable<void> {
    return this.http.delete<void>(`${BASE_URL}/${id}`);
  }
}