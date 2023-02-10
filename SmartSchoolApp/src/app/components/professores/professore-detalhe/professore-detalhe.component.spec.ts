import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessoreDetalheComponent } from './professore-detalhe.component';

describe('ProfessoreDetalheComponent', () => {
  let component: ProfessoreDetalheComponent;
  let fixture: ComponentFixture<ProfessoreDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfessoreDetalheComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfessoreDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
