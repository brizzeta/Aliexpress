export interface CategoryListItemDto {
    id: number;
    name: string;
    description?: string;
    parentCategoryId?: number;
    parentCategoryName?: string;
    productCount: number;
  }