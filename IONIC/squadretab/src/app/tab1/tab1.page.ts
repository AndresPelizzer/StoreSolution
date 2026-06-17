import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { SquadraService } from '../services/squadra';
import { Squadra } from '../models/squadra.model';
import { FormsModule } from '@angular/forms';
import { register } from 'swiper/element/bundle';
import { addIcons } from 'ionicons';
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
  imports: [IonicModule, FormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class Tab1Page implements OnInit {
  getDati() {
    this.squadraService.getDati().subscribe({
      next: (risposta: Squadra[]) => {
        this.dati = risposta;
      },
      error: (errore) => console.error(errore),
    });
  }

  dati: Squadra[] = [];
  squadraInModifica: number | null = null;
  nuovaSquadra: Squadra = {
    idsquadra: 0,
    nomeSquadra: '',
    citta: '',
    allenatore: '',
    numeroGiocatoriInRosa: 0,
    giocatori: [],
  };

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
  }

  ngOnInit(): void {
    this.getDati();
  }

  post() {
    if (
      !this.nuovaSquadra.nomeSquadra.trim() ||
      !this.nuovaSquadra.citta.trim() ||
      !this.nuovaSquadra.allenatore.trim()
    ) {
      alert(
        'Attenzione: devi compilare tutti i campi prima di aggiungere una squadra!',
      );
      return;
    }
    this.squadraService.postDati(this.nuovaSquadra).subscribe({
      next: () => {
        this.getDati();

        this.nuovaSquadra = {
          idsquadra: 0,
          nomeSquadra: '',
          citta: '',
          allenatore: '',
          numeroGiocatoriInRosa: 0,
          giocatori: [],
        };
      },
      error: (errore) => console.log('Errore:', errore),
    });
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
