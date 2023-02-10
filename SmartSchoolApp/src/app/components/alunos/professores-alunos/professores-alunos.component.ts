import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Disciplina } from 'src/app/model/Disciplina';
import { Professor } from 'src/app/model/Professor';
import { Util } from 'src/util/util';

@Component({
  selector: 'app-professores-alunos',
  templateUrl: './professores-alunos.component.html',
  styleUrls: ['./professores-alunos.component.css']
})
export class ProfessoresAlunosComponent implements OnInit {
  @Input() public professores: Professor[];
  @Output() closeModal = new EventEmitter();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  disciplinaConcat(disciplinas: Disciplina[]): string {
    return Util.nomeConcat(disciplinas);
  }

  professorSelect(prof: Professor): void {
    this.closeModal.emit(null);
    this.router.navigate(['/professor', prof.id]);
  }
}
