import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Stock } from '../user/dtos/stock';

@Injectable({
  providedIn: 'root'
})
export class TrendsService {
  constructor(private http: HttpClient) {
  }  

  public getStocks(): Observable<any> {
    return this.http.get<Stock[]>(`${environment.api_url}/trends`)
      .pipe(
        tap((stocks) => stocks),
        catchError(this.handleError<any>('Erro ao buscar lista de Ações'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} Error: ${error.message}`);
      return of(result as T);
    };
  }
}
