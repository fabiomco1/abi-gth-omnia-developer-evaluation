import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ProductsService } from './products.service';
import { Product } from './products.model';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './products.html'
})
export class Products implements OnInit {
  private service = inject(ProductsService);

  products: Product[] = [];
  produtosPaginados: Product[] = [];

  paginaAtual = 1;
  itemsPorPagina = 9;
  exibirFormulario = false;

  novoProduto: Product = {
    productName: '',
    description: '',
    category: '',
    image: '',
    price: 0,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString()
  };

  ngOnInit() {
    this.service.getProducts().subscribe((todosProdutos) => {
      this.products = todosProdutos;
      this.atualizarPagina();
    });
  }

  atualizarPagina() {
    const start = (this.paginaAtual - 1) * this.itemsPorPagina;
    const end = start + this.itemsPorPagina;
    this.produtosPaginados = this.products.slice(start, end);
  }

  trocarPagina(novaPagina: number) {
    this.paginaAtual = novaPagina;
    this.atualizarPagina();
  }

  selecionarImagem(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.novoProduto.image = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  salvarProduto() {
    this.novoProduto.createdAt = new Date().toISOString();
    this.novoProduto.updatedAt = new Date().toISOString();

    this.service.createProduct(this.novoProduto).subscribe((produtoCriado) => {
      this.products.push(produtoCriado);
      this.atualizarPagina();
      this.exibirFormulario = false;
      this.novoProduto = {
        productName: '',
        description: '',
        category: '',
        image: '',
        price: 0,
        createdAt: '',
        updatedAt: ''
      };
    });
  }
}