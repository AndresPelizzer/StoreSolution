import { Routes } from '@angular/router';
import { Seriea } from './seriea/seriea';
import { Serieb } from './serieb/serieb';
import { Tabellagiocatori } from './tabellagiocatori/tabellagiocatori';
import { DettaglioGiocatore } from './dettaglio-giocatore/dettaglio-giocatore';

export const routes: Routes = [
  { path: 'seriea', component: Seriea },
  { path: 'serieb', component: Serieb },
  { path: 'tabellagiocatori', component: Tabellagiocatori },
  { path: '', redirectTo: '/seriea', pathMatch: 'full' },
  { path: 'giocatori/:codice', component: DettaglioGiocatore },
];
