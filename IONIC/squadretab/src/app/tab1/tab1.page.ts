import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { SquadraService } from '../services/squadra';
import { Squadra } from '../models/squadra.model';
import { FormsModule } from '@angular/forms';
import { register } from 'swiper/element/bundle';
import { addIcons } from 'ionicons';
import { RouterModule } from '@angular/router';

import {
  pencil,
  trash,
  addOutline,
  checkmark,
  closeOutline,
  peopleOutline,
  locationOutline,
  personOutline,
} from 'ionicons/icons';
register();
@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  standalone: true,
  imports: [IonicModule, FormsModule, RouterModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class Tab1Page {
  getDati() {
    this.squadraService.getDati().subscribe({
      next: (risposta: Squadra[]) => {
        this.dati = risposta;
      },
      error: (errore) => console.error(errore),
    });
  }
  aggiungisquadra = false;

  dati: Squadra[] = [];
  squadraInModifica: number | null = null;

  constructor(private squadraService: SquadraService) {
    addIcons({
      pencil,
      trash,
      addOutline,
      checkmark,
      closeOutline,
      peopleOutline,
      locationOutline,
      personOutline,
    });
    this.squadraService.squadraAggiornata$.subscribe(() => {
      this.getDati();
    });
  }

  ionViewWillEnter() {
    this.getDati();
  }

  delete(idsquadra: number) {
    const conferma = confirm(
      `Sei sicuro di voler eliminare la squadra con ID ${idsquadra}?`,
    );
    if (conferma) {
      this.squadraService.deleteDati(idsquadra).subscribe({
        next: () =>
          (this.dati = this.dati.filter((s) => s.idsquadra !== idsquadra)),

        error: (errore) => console.log('Errore:', errore),
      });
    }
  }

  update(idsquadra: number, squadraModificata: Squadra) {
    this.squadraService.updateDati(idsquadra, squadraModificata).subscribe({
      next: () => {
        this.dati = this.dati.map((s) =>
          s.idsquadra === idsquadra ? squadraModificata : s,
        );
        this.squadraInModifica = null;
      },
      error: (errore) => console.log('Errore:', errore),
    });
  }
  modifica(idsquadra: number) {
    this.squadraInModifica = idsquadra;
  }
}
