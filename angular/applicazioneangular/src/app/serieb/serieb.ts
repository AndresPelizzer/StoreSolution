import { Component, inject, Inject } from '@angular/core';
import { Calcoli } from '../services/calcoli';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-serieb',
  imports: [FormsModule],
  templateUrl: './serieb.html',
  styleUrl: './serieb.css',
})
export class Serieb {
  private calcService = inject(Calcoli);
  contatore: number = 0;
  contgiocatori: number = 0;
  nome: string = '';
  aggiungi() {
    this.contatore = this.contatore + 1;
  }
  reset() {
    this.contatore = 0;
  }
  finiti: string = '';
  names: string[] = [];
  giocatori: string[] = ['Giacomo', 'Mario', 'Luigi', 'Pietro'];
  aggiungigiocatori() {
    if (this.contgiocatori >= this.giocatori.length) {
      return;
    }
    this.names.push(this.giocatori[this.contgiocatori]);
    this.contgiocatori = this.contgiocatori + 1;

    if (this.giocatori.length === this.contgiocatori) {
      this.finiti = 'I giocatori sono finiti';
    }
  }
  listagiocatori: string[] = [];

  nomegiocatore = '';
  aggiuntagiocatori() {
    if (this.nomegiocatore === '') {
      return;
    } else {
      this.listagiocatori.push(this.nomegiocatore);
      this.nomegiocatore = '';
    }
  }
  elimina(index: number) {
    this.listagiocatori.splice(index, 1);
  }
  public risultato: number | string = '';
  operazione: string = '';
  numero1: number | null = null;
  numero2: number | null = null;
  public calcola() {
    if (this.numero1 === null || this.numero2 === null) {
      alert('Inserisci entrambi i numeri!');
      return;
    }
    if (isNaN(this.numero1) || isNaN(this.numero2)) {
      alert('Inserisci solo numeri!');
      return;
    }
    this.risultato = this.calcService.calcolatrice(this.numero1!, this.numero2!, this.operazione);
  }
}
