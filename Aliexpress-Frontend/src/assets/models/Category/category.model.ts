export interface CategoryDto {
    id: number;
    name: string;
    description?: string;
    parentCategoryId?: number;
    parentCategoryName?: string;
    childCategories: CategoryDto[];
  }