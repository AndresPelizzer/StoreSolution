import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Squadra } from '../models/squadra.model';
import { Observable, Subject } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class SquadraService {
  nuovasquadra: Squadra[] = [];

  // private apiUrl = 'http://pc-stage:82/api/squadre';
  private apiUrl = 'http://mtswebtest:86/api/squadre';

  constructor(private http: HttpClient) {}

  private squadraAggiornataSource = new Subject<void>();
  squadraAggiornata$ = this.squadraAggiornataSource.asObservable();

  notificaAggiornamento() {
    this.squadraAggiornataSource.next();
  }

  getDati(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
  postDati(nuovasquadra: Squadra) {
    return this.http.post<Squadra>(this.apiUrl, nuovasquadra);
  }
  deleteDati(idsquadra: number) {
    return this.http.delete(`${this.apiUrl}/${idsquadra}`);
  }
  updateDati(idsquadra: number, nuovaSquadra: Squadra) {
    return this.http.put(`${this.apiUrl}/${idsquadra}`, nuovaSquadra);
  }
}
