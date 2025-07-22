export interface Product {
  productName: string;
  description: string;
  category: string;
  image: string;
  price: number;
  createdAt: string;
  updatedAt: string;
}

export interface ApiEnvelope<T> {
  success: boolean;
  data: T;
  message: string;
  errors: any[];
}
