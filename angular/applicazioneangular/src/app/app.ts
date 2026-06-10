import { Component, signal } from '@angular/core';
// AGGIUNTO: RouterLink e RouterLinkActive importati da @angular/router
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { Seriea } from './seriea/seriea';
import { Serieb } from './serieb/serieb';
import { Tabellagiocatori } from './tabellagiocatori/tabellagiocatori';
import { DettaglioGiocatore } from './dettaglio-giocatore/dettaglio-giocatore';

@Component({
  selector: 'app-root',
  standalone: true,

  imports: [
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    Seriea,
    Serieb,
    Tabellagiocatori,
    DettaglioGiocatore,
  ],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('applicazioneangular');
}
