import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  public eventos: Evento[] = [];
  public eventosFiltrado: Evento[] = [];
  public eventoId = 0;
  public modalRef?: BsModalRef;
  public widthImg = 150;
  public marginImg = 2;
  public isCollapsed = true;
  private filtroListado = '';

  public get filtroLista() : string{
    return this.filtroListado;
  }

  public set filtroLista(value: string) {
    this.filtroListado = value;
    this.eventosFiltrado = this.filtroListado ? this.filtrarLista(this.filtroListado) : this.eventos;
  }

  public filtrarLista(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter((evento: any) =>
      evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({
      next: (eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrado = this.eventos;
      },
      error: () => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar o eventos.','ERRO!');
      }
    }).add(() => this.spinner.hide());
  }

  public eventoDetalhe(id: number): void {
    this.router.navigate([`eventos/detalhe/${id}`]);
  }

  public openModal(event: Event, template: TemplateRef<any>, eventoId: number): void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();
    this.eventoService.deleteEvento(this.eventoId).subscribe({
      next: (result: any) => {
        this.toastr.success(result.message)
        this.spinner.hide();
        this.getEventos();
      },
      error: (error: Error) => {
        console.error(error);
        this.toastr.error('Ocorreu um erro ao deletar o evento.');
      }
    }).add(() => this.spinner.hide());
  }

  public decline(): void {
    this.modalRef?.hide();
  }

}
