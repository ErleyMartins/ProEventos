import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Evento } from 'src/app/models/Evento';

@Component({
  selector: 'app-evento-detalhes',
  templateUrl: './evento-detalhes.component.html',
  styleUrls: ['./evento-detalhes.component.scss']
})
export class EventoDetalhesComponent implements OnInit {

  public form!: FormGroup;
  public evento = {} as Evento;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  get f(): any { return this.form.controls; }

  private validation(): void {
    this.form = this.fb.group({
      id: [],
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      imagemURL: [''],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([]),
      redesSociais: this.fb.array([])
    });
  }

  public submit(): void {
    console.log(this.form.value);
  }
}
