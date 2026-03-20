export interface MenuCategory {
  id: number;
  name: string;
  sortOrder: number;
  restaurantId: number;
}
export interface CreateMenuCategoryDto {
  name: string;
  sortOrder: number;
  restaurantId: number;
}
