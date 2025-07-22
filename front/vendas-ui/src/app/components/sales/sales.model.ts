export interface SaleProduct {
  id?: string;             
  saleNumber: string;
  productId: string;
  quantity: number;
  totalItemAmount?: number;
  discount?: number;
}

export interface Sale {
  id?: string;        
  saleNumber: string;
  saleDate?: string;
  customer: string;
  totalSaleAmount?: number;
  totalItems?: number;
  branch: string;
  cancelled?: boolean;
  createdAt?: string;
  updatedAt?: string;
  cancelledAt?: string;
  salesProducts?: SaleProduct[];
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}