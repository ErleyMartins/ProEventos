import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-detalhes',
  templateUrl: './evento-detalhes.component.html',
  styleUrls: ['./evento-detalhes.component.scss']
})
export class EventoDetalhesComponent implements OnInit {

  public form!: FormGroup;
  public evento = {} as Evento;
  private metedo = 'post';

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private route: ActivatedRoute,
              private eventoService: EventoService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService
              )
    {
      this.localeService.use('pt-br');
    }

  ngOnInit() {
    this.carregaEvento();
    this.validation();
  }

  public get f(): any { return this.form.controls; }

  public get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY HH:mm',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }

  public carregaEvento(): void {
    this.spinner.show();
    const eventoIdParam = this.route.snapshot.paramMap.get('id');

    if (eventoIdParam !== null) {
      this.eventoService.getEventoById(+eventoIdParam).subscribe({
        next: (evento: Evento) => {
          this.evento = {...evento};
          this.metedo = 'put';
          console.log(this.evento);
          this.form.patchValue(this.evento);
        },
        error: (error: Error) => {
          console.error(error);
          this.toastr.error('Erro ao carregar o evento.');
        }
      }).add(() => this.spinner.hide());
    }
    else this.spinner.hide();
  }

  private validation(): void {
    this.form = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      imagemURL: [''],
      qtdPessoa: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([]),
      redesSociais: this.fb.array([])
    });
  }

  public submit(): void {
    console.log(this.form.value);
  }

  public cssValidators(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    this.evento = (this.metedo === 'post') ? {... this.form.value } : { id: this.evento.id, ... this.form.value };

    this.eventoService[this.metedo](this.evento).subscribe({
      next: (evento: Evento) => this.toastr.success('Evento atualizado com sucesso!'),
      error: (error: Error) => {
        console.error(error);
        this.toastr.error(`Erro ao atualizar o evento!`);
      }
    }).add(() => this.spinner.hide());
  }
}
