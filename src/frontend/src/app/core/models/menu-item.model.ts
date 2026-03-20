export interface MenuItem {
  id: number;
  name: string;
  description?: string;
  price: number;
  menuCategoryId: number;
  imageUrl?: string;
  isAvailable: boolean;
}
export interface CreateMenuItemDto {
  name: string;
  description?: string;
  price: number;
  menuCategoryId: number;
  imageUrl?: string;
  isAvailable: boolean;
}
