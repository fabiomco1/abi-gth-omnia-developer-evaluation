import { Component, inject } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SalesService } from './sales.service';
import { Sale } from './sales.model';

@Component({
  selector: 'app-sales',
  standalone: true,
  imports: [CommonModule, FormsModule, DatePipe],
  templateUrl: './sales.html'
})
export class SalesComponent {
  private service = inject(SalesService);

  // Lista de vendas carregadas
  sales: Sale[] = [];

  // Controla exibição do formulário de nova venda
  exibirFormulario = false;

  // Objeto que representa a nova venda a ser cadastrada
  novaVenda: Sale = {
    saleNumber: '',
    saleDate: new Date().toISOString(),
    customer: '',
    branch: '',
    salesProducts: []
  };

  ngOnInit() {
  }

gerarVendasFake() {
  this.service.createFakeSales().subscribe({
    next: (res) => {
      if (res.success) {
        alert('Vendas de teste criadas com sucesso!');
        this.buscarVendas(); // Recarrega após gerar
      } else {
        alert('Erro: ' + res.message);
      }
    },
    error: (err) => {
      console.error(err);
      alert('Erro ao gerar vendas fake!');
    }
  });
}

buscarVendas() {
  this.service.getAllSales().subscribe({
    next: (lista) => {
      this.sales = lista;
    },
    error: (err) => {
      console.error(err);
      alert('Erro ao buscar vendas!');
    }
  });
}


  // Envia os dados para o backend e cadastra a venda
  vender() {
    if (!this.novaVenda.saleNumber) {
      this.novaVenda.saleNumber = 'VENDA-' + Math.floor(Math.random() * 10000);
    }

    this.service.createSale(this.novaVenda).subscribe({
      next: (res) => {
        if (res.success) {
          alert('Venda cadastrada com sucesso!');
          this.sales.push(this.novaVenda);
          this.resetarFormulario();
        } else {
          alert('Erro: ' + res.message);
        }
      },
      error: (err) => {
        console.error(err);
        alert('Erro ao cadastrar a venda!');
      }
    });
  }

  // Limpa e fecha o formulário
  private resetarFormulario() {
    this.novaVenda = {
      saleNumber: '',
      saleDate: new Date().toISOString(),
      customer: '',
      branch: '',
      salesProducts: []
    };
    this.exibirFormulario = false;
  }
}