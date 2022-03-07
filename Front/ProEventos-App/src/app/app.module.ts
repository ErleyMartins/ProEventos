import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// NGX
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';

// APP
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { EventoDetalhesComponent } from './components/eventos/evento-detalhes/evento-detalhes.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { PalestrantesComponent } from './components/palestrantes/palestrantes.component';
import { NavComponent } from './shared/nav/nav.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { ContatoComponent } from './components/contato/contato.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

// Service
import { EventoService } from './services/evento.service';

// PIPE
import { DateTimeFormatPipe } from './helpers/DateTimeFormat.pipe';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';

@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    PalestrantesComponent,
    NavComponent,
    TituloComponent,
    ContatoComponent,
    PerfilComponent,
    DashboardComponent,
    DateTimeFormatPipe,
    EventoDetalhesComponent,
    EventoListaComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    })
  ],
  providers: [
    EventoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
