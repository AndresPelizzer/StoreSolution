import { Service } from '@angular/core';

@Service()
export class Calcoli {
  public test(x: number): string {
    return 'hai inserito: ' + x.toString();
  }

  public calcolatrice(num1: number, num2: number, operazione: string): number {
    if (operazione == '+') {
      return num1 + num2;
    } else if (operazione == '-') {
      return num1 - num2;
    } else if (operazione == '*') {
      return num1 * num2;
    } else if (operazione == '/') {
      if (num2 == 0) {
        alert('Operazione impossibile,riprova');
        return 0;
      } else {
        return num1 / num2;
      }
    } else {
      alert('Operazione non riconosciuta');
      return 0;
    }
  }

  public somma(num1: number, num2: number): number {
    return num1 + num2;
  }

  public differenza(num1: number, num2: number): number {
    return num1 - num2;
  }
}
