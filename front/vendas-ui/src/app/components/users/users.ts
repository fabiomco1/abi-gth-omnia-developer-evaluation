import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsersService, User } from './users.services';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './users.html'
})
export class Users {
  private service = inject(UsersService);

  users: User[] = [];

  // Usuário em criação
  novoUsuario: User = {
    username: '',
    password: '',
    phone: '',
    email: '',
    status: 0,
    role: 0
  };

  ngOnInit() {
    this.carregarUsuarios();
  }

  // Lista todos os usuários
  carregarUsuarios() {
    this.service.getAllUsers().subscribe({
      next: res => this.users = res,
      error: err => {
        console.error('Erro ao carregar usuários:', err);
        alert('Erro ao carregar lista de usuários');
      }
    });
  }

  // Cadastra novo usuário
  cadastrarUsuario() {
    this.service.createUser(this.novoUsuario).subscribe({
      next: () => {
        alert('Usuário cadastrado com sucesso!');
        this.carregarUsuarios();
        this.novoUsuario = {
          username: '',
          password: '',
          phone: '',
          email: '',
          status: 0,
          role: 0
        };
      },
      error: err => {
        console.error('Erro ao cadastrar usuário:', err);
        alert('Erro ao cadastrar usuário!');
      }
    });
  }

  // Gera usuários de teste
  gerarUsuariosFake() {
    this.service.createFakeUsers().subscribe({
      next: () => {
        alert('Usuários de teste criados!');
        this.carregarUsuarios();
      },
      error: err => {
        console.error('Erro ao criar usuários fake:', err);
        alert('Erro ao gerar usuários!');
      }
    });
  }

  // Exclui usuário pelo ID
  excluirUsuario(id?: string) {
    if (!id) return;

    if (confirm('Tem certeza que deseja excluir este usuário?')) {
      this.service.deleteUser(id).subscribe({
        next: () => {
          alert('Usuário excluído com sucesso!');
          this.carregarUsuarios();
        },
        error: err => {
          console.error('Erro ao excluir usuário:', err);
          alert('Erro ao excluir usuário!');
        }
      });
    }
  }

  // Visualiza detalhes de um usuário (pode ser estendido)
  verUsuario(id?: string) {
    if (!id) return;

    this.service.getUserById(id).subscribe({
      next: user => {
        alert(`Usuário: ${user.username}\nEmail: ${user.email}\nTelefone: ${user.phone}`);
      },
      error: err => {
        console.error('Erro ao buscar usuário:', err);
        alert('Erro ao buscar usuário!');
      }
    });
  }
}