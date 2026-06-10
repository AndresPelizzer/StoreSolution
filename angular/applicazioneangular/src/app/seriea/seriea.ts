import { Component, inject } from '@angular/core';
import { Calcoli } from '../services/calcoli';

@Component({
  selector: 'app-seriea',
  imports: [],
  templateUrl: './seriea.html',
  styleUrl: './seriea.css',
})
export class Seriea {
  private calcService = inject(Calcoli);

  prova1: string = 'Questo e un testo...';
  prova2: number = 100;

  prova3: string[] = ['elem1', 'elem2', 'elem3'];

  prova4 = {
    nome: 'Mario',
    cognome: 'rossi',
  };

  public prova5: string = '';

  public funzione1(dato1: number, dato2: number): number {
    const result = dato1 + dato2;
    return result;
  }

  public funzione2() {
    //this.prova5 = 'Funziona!!!';

    const num1: number = 12;

    this.prova5 = this.calcService.test(num1);
  }

  public funzione3(dato1: string, dato2: string): string {
    console.log('primo dato:', dato1);
    console.log('secondo dato:', dato2);

    const result = dato1 + dato2;

    console.log('risultato:', result);
    return result;
  }
}
