import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './screens/login/login.component';
import { HomeComponent } from './screens/home/home.component';
import { ListaFuncionariosComponent } from './screens/lista-funcionarios/lista-funcionarios.component';
import { CadastrarColaboradorComponent } from './screens/cadastrar-colaborador/cadastrar-colaborador.component';
import { EditarCadastroComponent } from './screens/editar-cadastro/editar-cadastro.component';
import { AlterarSenhaComponent } from './screens/alterar-senha/alterar-senha.component';
import { DetalhesColaboradorComponent } from './screens/detalhes-colaborador/detalhes-colaborador.component';
import { authenticatedGuard } from './guards/authenticated.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [authenticatedGuard]
  },
  {
    path: 'cadastro',
    component: ListaFuncionariosComponent,
    canActivate: [authenticatedGuard]
  },
  {
    path: 'cadastro/colaborador',
    component: CadastrarColaboradorComponent,
    canActivate: [authenticatedGuard]
  },
  {
    path: 'alterarSenha',
    component: AlterarSenhaComponent,
    canActivate: [authenticatedGuard]
  },
  {
    path: 'editar/:id',
    component: EditarCadastroComponent,
    canActivate: [authenticatedGuard]
  },
  {
    path: 'detalhes/:id',
    component: DetalhesColaboradorComponent,
    canActivate: [authenticatedGuard]
  },
  {
    path: '**',
    component: LoginComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
