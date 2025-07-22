import { Component, inject } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SalesService } from './sales.service';
import { Sale, SaleProduct } from './sales.model';

@Component({
  selector: 'app-sales',
  standalone: true,
  imports: [CommonModule, FormsModule, DatePipe],
  templateUrl: './sales.html'
})
export class Sales {
  private service = inject(SalesService);

  sales: Sale[] = [];
  exibirFormulario = false;

  novaVenda: Partial<Sale> = {
    saleNumber: '',
    saleDate: new Date().toISOString(),
    customer: '',
    branch: '',
    salesProducts: []
  };

  ngOnInit() {
    this.carregarVendas();
  }

  carregarVendas() {
    this.service.getAllSales().subscribe(v => this.sales = v);
  }

gerarVendasFake() {
  this.service.createFakeSales().subscribe({
    next: (res) => {
      if (res.success) {
        alert('Vendas de teste criadas com sucesso!');
        this.carregarVendas(); 
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

  vender() {
    this.service.createSale(this.novaVenda).subscribe(() => {
      this.exibirFormulario = false;
      this.carregarVendas(); 
      this.novaVenda = {
        saleNumber: '',
        saleDate: new Date().toISOString(),
        customer: '',
        branch: '',
        salesProducts: []
      };
    });
  }
}