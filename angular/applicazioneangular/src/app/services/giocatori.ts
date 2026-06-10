import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Giocatore } from '../models/giocatore';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Giocatori {
  private apiUrl = 'http://localhost:3000/users';
  public getGiocatori(): Observable<Giocatore[]> {
    return this.http.get<Giocatore[]>(this.apiUrl);
  }
  public deleteGiocatori(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  public getGiocatore(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  public addGiocatore(newValues: Giocatore): Observable<any> {
    const { id, ...senzaId } = newValues;
    return this.http.post(this.apiUrl, newValues);
  }
  public updateGiocatore(id: string, newValues: Giocatore): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, newValues);
  }

  constructor(private http: HttpClient) {}
}
