import { Component } from '@angular/core';
import { IonicModule, NavController } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { SquadraService } from '../../services/squadra';
import { Squadra } from '../../models/squadra.model';
import { ActivatedRoute } from '@angular/router';
import { addIcons } from 'ionicons';
import {
  checkmark,
  locationOutline,
  personOutline,
  peopleOutline,
} from 'ionicons/icons';

@Component({
  selector: 'app-modifica-squadra',
  templateUrl: './modifica-squadra.page.html',
  standalone: true,
  imports: [IonicModule, FormsModule],
})
export class ModificaSquadraPage {
  squadraModificata: Squadra = {
    idsquadra: 0,
    nomeSquadra: '',
    citta: '',
    allenatore: '',
    numeroGiocatoriInRosa: 0,
    giocatori: [],
  };

  constructor(
    private squadraService: SquadraService,
    private navCtrl: NavController,
    private route: ActivatedRoute,
  ) {
    addIcons({ checkmark, locationOutline, personOutline, peopleOutline });
  }

  ionViewWillEnter() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.squadraService.getById(id).subscribe({
      next: (squadra: Squadra) => {
        this.squadraModificata = squadra;
      },
      error: (errore) => console.error(errore),
    });
  }

  update() {
    this.squadraService
      .updateDati(this.squadraModificata.idsquadra, this.squadraModificata)
      .subscribe({
        next: () => {
          this.navCtrl.back();
        },
        error: (errore) => console.log('Errore:', errore),
      });
  }
}
