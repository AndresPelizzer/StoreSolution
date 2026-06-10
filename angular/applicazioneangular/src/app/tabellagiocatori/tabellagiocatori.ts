import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import { Giocatore } from '../models/giocatore';
import { Giocatori } from '../services/giocatori';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tabellagiocatori',
  imports: [],
  templateUrl: './tabellagiocatori.html',
  styleUrl: './tabellagiocatori.css',
})
export class Tabellagiocatori implements OnInit {
  listagiocatori: Giocatore[] = [];
  giocatoreOriginale: Giocatore | null = null;

  private caricaLista() {
    this.gService.getGiocatori().subscribe((data) => {
      if (data) {
        this.listagiocatori = data; // Popola correttamente l'array della tabella
        this.cd.detectChanges();
      }
    });
  }

  nuovoGiocatore: Giocatore = {
    id: '',
    name: '',
    username: '',
    email: '',
  };
  indiceModifica: number | null = null;

  ngOnInit() {
    this.caricaLista();
  }

  public elimina(id: string) {
    this.gService.deleteGiocatori(id).subscribe(() => {
      this.caricaLista();
    });
  }

  public aggiungiGiocatore() {
    if (this.indiceModifica != null) {
      this.gService
        .updateGiocatore(this.listagiocatori[this.indiceModifica].id, this.nuovoGiocatore)
        .subscribe(() => {
          this.caricaLista();
        });
      this.indiceModifica = null;
    } else {
      this.gService.addGiocatore(this.nuovoGiocatore).subscribe(() => {
        this.caricaLista();
      });
    }
    this.nuovoGiocatore.name = '';
    this.nuovoGiocatore.username = '';
    this.nuovoGiocatore.email = '';
  }

  public modifica(index: number) {
    this.indiceModifica = index;
    this.giocatoreOriginale = { ...this.listagiocatori[index] };
    this.nuovoGiocatore = { ...this.listagiocatori[index] };
  }

  public annulla() {
    if (this.giocatoreOriginale != null) {
      this.nuovoGiocatore = { ...this.giocatoreOriginale };
    }
    this.indiceModifica = null;
  }

  // Cambiato da 'number' a 'string' per allinearsi al database e al template HTML
  public mostra(id: string) {
    this.router.navigate(['/giocatori', id]);
  }

  constructor(
    private gService: Giocatori,
    private cd: ChangeDetectorRef,
    private router: Router,
  ) {}
}
