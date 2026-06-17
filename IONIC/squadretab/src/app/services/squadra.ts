import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Squadra } from '../models/squadra.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class SquadraService {
  nuovasquadra: Squadra[] = [];

  private apiUrl = 'http://pc-stage:82/api/squadre';
  constructor(private http: HttpClient) {}

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
    return this.http.put(
      `http://pc-stage:82/api/squadre/${idsquadra}`,
      nuovaSquadra,
    );
  }
}
