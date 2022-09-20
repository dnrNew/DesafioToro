import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable, of, tap } from 'rxjs';
import { Order } from '../user/dtos/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private environment = { api_url: `https://localhost:7109` }

  constructor(
    private http: HttpClient,
    private toastr: ToastrService) {
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

      if (error.status == 400 && error.error == "Saldo Insuficiente")
        this.toastr.error(error.error);
      else
        console.error(`${operation} Error: ${error.message}`);

      return of(result as T);
    };
  }
}
