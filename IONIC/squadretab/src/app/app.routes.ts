import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: '',
    loadChildren: () => import('./tabs/tabs.routes').then((m) => m.routes),
  },
  {
    path: 'aggiungi-squadra',
    loadComponent: () =>
      import('./pages/aggiungi-squadra/aggiungi-squadra.page').then(
        (m) => m.AggiungiSquadraPage,
      ),
  },
  {
    path: 'modifica-squadra',
    loadComponent: () =>
      import('./pages/modifica-squadra/modifica-squadra.page').then(
        (m) => m.ModificaSquadraPage,
      ),
  },
  {
    path: 'home',
    loadComponent: () =>
      import('./pages/home/home.page').then((m) => m.HomePage),
  },
];
