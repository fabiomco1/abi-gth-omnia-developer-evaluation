export interface User {
  id: string;
  name: string;
  email: string;
  phone: string;
  role: number;
  status: number;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}
