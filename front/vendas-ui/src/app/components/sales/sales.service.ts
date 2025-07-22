import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ApiResponse, Sale } from './sales.model';

const BASE_URL = 'https://localhost:7181/api/Sales';

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private http = inject(HttpClient);

  // Cria uma venda nova
  createSale(sale: Partial<Sale>): Observable<ApiResponse<Sale>> {
    return this.http.post<ApiResponse<Sale>>(`${BASE_URL}/SaleCreated`, sale);
  }

  // Obtém uma venda pelo ID
  getSale(id: string): Observable<ApiResponse<Sale>> {
    return this.http.get<ApiResponse<Sale>>(`${BASE_URL}/GetSale/${id}`);
  }

  // Cancela uma venda pelo ID
  cancelSale(id: string): Observable<ApiResponse<Sale>> {
    return this.http.get<ApiResponse<Sale>>(`${BASE_URL}/SaleCancelled/${id}`);
  }

  // Cria vendas fake para teste
  createFakeSales(): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${BASE_URL}/CreateSaleTest`, {});
  }

  getAllSales(): Observable<Sale[]> {
    return this.http
      .get<ApiResponse<{ data: Sale[] }>>(`${BASE_URL}/ListAll`)
      .pipe(map(res => res.data.data));
  }
}