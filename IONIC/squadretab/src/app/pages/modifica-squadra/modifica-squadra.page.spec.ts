import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ModificaSquadraPage } from './modifica-squadra.page';

describe('ModificaSquadraPage', () => {
  let component: ModificaSquadraPage;
  let fixture: ComponentFixture<ModificaSquadraPage>;

  beforeEach(() => {
    fixture = TestBed.createComponent(ModificaSquadraPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
