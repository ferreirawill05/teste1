import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatInputModule } from '@angular/material/input';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './screens/login/login.component';
import { HomeComponent } from './screens/home/home.component';
import { AlterarSenhaComponent } from './screens/alterar-senha/alterar-senha.component';
import { ListaFuncionariosComponent } from './screens/lista-funcionarios/lista-funcionarios.component';
import { CadastrarColaboradorComponent } from './screens/cadastrar-colaborador/cadastrar-colaborador.component';
import { EditarCadastroComponent } from './screens/editar-cadastro/editar-cadastro.component';
import { HeaderComponent } from './screens/comp/header/header.component';
import { RouterModule, Routes } from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatIconModule} from '@angular/material/icon';
import { DetalhesColaboradorComponent } from './screens/detalhes-colaborador/detalhes-colaborador.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatDialogModule} from '@angular/material/dialog';
import { DoubleMsgComponent } from './screens/comp/double-msg/double-msg.component';
import { MsgErrorComponent } from './screens/comp/msg-error/msg-error.component';





const routes : Routes = [
  {path: 'Home', component: HomeComponent},
  {path: 'Cadastro', component: CadastrarColaboradorComponent},
  {path: 'Login', component: LoginComponent},
  {path: 'AlterarSenha', component: AlterarSenhaComponent},
  {path: 'EditarCadastro', component: EditarCadastroComponent},
  {path: 'ListaFuncionarios', component: ListaFuncionariosComponent},
  {path: 'DetalhesColaborador', component: DetalhesColaboradorComponent}


]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AlterarSenhaComponent,
    ListaFuncionariosComponent,
    CadastrarColaboradorComponent,
    EditarCadastroComponent,
    HeaderComponent,
    DetalhesColaboradorComponent,
    DoubleMsgComponent,
    MsgErrorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes),
    MatButtonModule,
    MatTableModule,
    MatMenuModule,
    MatToolbarModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatIconModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatPaginatorModule,
    MatDialogModule,
    MatNativeDateModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
