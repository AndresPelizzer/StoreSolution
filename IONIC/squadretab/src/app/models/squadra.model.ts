export interface Giocatore {
  idgiocatore: number;
  nome: string;
  cognome: string;
  ruolo: string;
  idsquadra: number;
}
export interface Squadra {
  idsquadra: number;
  nomeSquadra: string;
  citta: string;
  allenatore: string;
  numeroGiocatoriInRosa: number;
  giocatori: Giocatore[];
}
