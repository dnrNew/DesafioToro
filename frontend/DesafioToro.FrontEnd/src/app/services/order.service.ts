import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { Order } from '../user/dtos/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private environment = { api_url: `https://localhost:7109` }

  constructor(
    private http: HttpClient) {
  }

  public executeOrder(order: Order, userId: number): Observable<any> {
    let httpOptions = {
      headers: new HttpHeaders({
        'userId': userId.toString(),
        'Content-Type': 'application/json'
      }),      
      responseType: 'text' as 'json'
    };

    return this.http.post<string>(`${this.environment.api_url}/order`, order, httpOptions)
      .pipe(
        tap((response) => response),
        catchError(this.handleError<any>('Erro ao executar a ordem'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} Error: ${error.message}`);
      return of(result as T);
    };
  }
}
