import { Routes } from '@angular/router';
import { TabsPage } from './tabs.page';

export const routes: Routes = [
  {
    path: 'tabs',
    component: TabsPage,
    children: [
      {
        path: 'tab1',
        loadComponent: () =>
          import('../tab1/tab1.page').then((m) => m.Tab1Page),
      },
      // Inserisci qui la rotta come figlia di tabs
      {
        path: 'aggiungi-squadra',
        loadComponent: () =>
          import('../pages/aggiungi-squadra/aggiungi-squadra.page').then(
            (m) => m.AggiungiSquadraPage,
          ),
      },
      {
        path: 'modifica-squadra/:id',
        loadComponent: () =>
          import('../pages/modifica-squadra/modifica-squadra.page').then(
            (m) => m.ModificaSquadraPage,
          ),
      },
      {
        path: 'tab2',
        loadComponent: () =>
          import('../tab2/tab2.page').then((m) => m.Tab2Page),
      },
      {
        path: 'tab3',
        loadComponent: () =>
          import('../tab3/tab3.page').then((m) => m.Tab3Page),
      },
      {
        path: '',
        redirectTo: '/tabs/tab1',
        pathMatch: 'full',
      },
    ],
  },
  {
    path: '',
    redirectTo: '/tabs/tab1',
    pathMatch: 'full',
  },
];
