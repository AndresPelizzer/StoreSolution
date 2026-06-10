import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { Giocatori } from '../services/giocatori';
import { Giocatore } from '../models/giocatore';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dettaglio-giocatore',
  imports: [RouterLink],
  templateUrl: './dettaglio-giocatore.html',
  styleUrl: './dettaglio-giocatore.css',
})
export class DettaglioGiocatore implements OnInit {
  giocatore: Giocatore | null = null;
  codice: string | null = '';
  nuovoGiocatore: Giocatore = {
    id: '',
    name: '',
    username: '',
    email: '',
  };
  constructor(
    private route: ActivatedRoute,
    private gService: Giocatori,
    private cd: ChangeDetectorRef,
    private router: Router,
  ) {}

  ngOnInit() {
    this.codice = this.route.snapshot.paramMap.get('codice');

    if (this.codice) {
      this.gService.getGiocatore(this.codice).subscribe({
        next: (data) => {
          console.log('Dati ricevuti dal server:', data);

          if (data && data.id) {
            this.giocatore = data;

            this.nuovoGiocatore = { ...data };

            this.cd.detectChanges();
          } else {
            console.warn('Giocatore non trovato nel database.');
          }
        },
        error: (err) => {
          console.error('Errore nella chiamata HTTP:', err);
        },
      });
    }
  }

  public modifica(codice: string | null) {
    if (codice && this.giocatore) {
      this.gService.updateGiocatore(codice, this.nuovoGiocatore).subscribe({
        next: (risposta) => {
          console.log('Giocatore aggiornato con successo:', risposta);
          this.giocatore = { ...this.nuovoGiocatore };
        },
        error: (err) => {
          console.error('Errore durante la modifica:', err);
        },
      });
    } else {
      console.warn('Impossibile modificare: codice o dati del giocatore mancanti.');
    }
  }

  public elimina(codice: string | null) {
    if (codice && this.giocatore) {
      this.gService.deleteGiocatori(codice).subscribe({
        next: () => {
          console.log('Giocatore eliminato con successo dal database');
          this.router.navigate(['/tabellagiocatori']);
        },
        error: (err) => {
          console.error("Errore durante l'eliminazione:", err);
        },
      });
    }
  }
  public annulla() {
    if (this.giocatore) this.nuovoGiocatore = { ...this.giocatore };
  }
}
