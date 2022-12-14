import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from '../user/dtos/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) {
  }

  public getUser(userId: number): Observable<any> {
    return this.http.get<User>(`${environment.api_url}/user/${userId}`)
      .pipe(
        tap((user) => user),
        catchError(this.handleError<any>('Erro ao buscar detalhes do Usuário'))
      );
  }

  public getAllUsers(): Observable<any> {
    return this.http.get<User[]>(`${environment.api_url}/user`)
      .pipe(
        tap((users) => users),
        catchError(this.handleError<any>('Erro ao buscar lista de Usuários'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} Error: ${error.message}`);
      return of(result as T);
    };
  }
}
