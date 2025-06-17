export interface CategoryCreateDto {
    name: string;
    description?: string;
    parentCategoryId?: number;
  }