import { Component, ViewChild, ElementRef } from '@angular/core';
import { NgFor, NgClass, CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [NgFor, NgClass, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  // Флаги активности для выпадающих окон
  isCatalogActive = false;
  isLangActive = false;
  isMenuActive = false;
  // Флаги для анимации "исчезновения" (fade-out)
  isCatalogFadingOut = false;
  isLangFadingOut = false;
  isMenuFadingOut = false;
  // Флаг для отображения категорий
  isCategoriesVisible = false;

  selectedCategory = 'Mobile phones'; // Выбранная категория каталога
  categories = [ // Список всех категорий
    'Mobile phones',
    'Computers',
    'Electronics',
    'Home Appliances',
    'Home Improvement & Tools',
    'Security & Protection',
    'Automobiles & Motorcycles',
    'Home, Garden & Office',
    'Furniture',
    'Clothing',
    'Shoes',
    'Accesoires',
    'Luggage & Bags',
    'Sports & Entertainment',
    'Mother & Kids',
    'Toys & Hobbies',
    'Beauty & Health',
    '18+',
    'Pet Products',
    'Food'
  ];
  catalogContent: { [key: string]: string[] } = { // Содержимое каталога для каждой категории
    'Mobile phones': ['Mobile Phones', 'Mobile Phones Accessories', 'Mobile Phones Parts', 'Sim Cards & Accessories', 'Sattelite Phones'],
    'Computers': ['Laptops', 'Tablets', 'Desktops & AIO', 'Networking', 'Servers & Industrial Computer', 'Storage Device', 'Computers Patrs & Accessories'],
    'Electronics': ['Audio & Video', 'Camera & Photo', 'Electronic Components & Supplies', 'Wearable Smart Devices', 'Smart Remote Control', '3D Printing & Additive Manufacturing', 'Games & Accessories', 'Office Electronics', 'Walkie Talkie', 'Camera Drones & Accessories', 'E-Book Readers', 'Smart Electronics', 'Communication Equipment', 'Public Broadcasting', 'Robot'],
    'Home Appliances': ['Cleaning Appliances', 'Kitchen Appliances', 'Home Comfort Appliance', 'Household Appliances', 'Major Appliances', 'Built-In Appliances', 'Commerctial Appliances', 'Compact/Portable', 'Home Appliance Parts'],
    'Home Improvement & Tools': ['Home Improvement', 'Tools'],
    'Security & Protection': ['Video Surveillance', 'Self Defense Supplies', 'Access Control', 'Workplace Safety Supplies', 'Intercom', 'Security Alarm', 'Building Automation', 'IoT Devices', 'Transmission & Cables', 'Fire Protection', 'Safes', 'Disaster-Relief Supplies', 'Roadway Safety', 'Security Inspection Device', 'Smart Card System'],
    'Automobiles & Motorcycles': ['Equipments', 'Automobiles, Parts & Accessories', 'Motorcycles, Parts & Accessories', 'Other Vehicle Parts & Accessories'],
    'Home, Garden & Office': ['Lights & Lighting', 'Kitchen, Dining & Bar', 'Household Merchandises', 'Festive & Party Supplies', 'Home Decor', 'Home Storage & Organization', 'Home Textile', 'Office & School Supplies', 'Garden Supplies', 'Books & Magazines'],
    'Furniture': ['Office Furniture', 'Home Furniture', 'Outdoor Furniture', 'Commercial Furniture', 'Furniture Accessories', 'Furniture Parts'],
    'Clothing': ['Women\'s Clothing', 'Men\'s Clothing', 'Children\'s Clothing', 'Novelty & Special Use', 'Baby Clothing'],
    'Shoes': ['Women\'s Shoes', 'Men\'s Shoes', 'Sport Shoes', 'Children\'s Shoes', 'Dance', 'Baby Shoes', 'Shoe Accessories'],
    'Accesoires': ['Jewelry & Accesoires', 'Apparel Accesoires', 'Watches', 'Eyewear & Accesoires', 'Headwear', 'Scarves & Wraps', 'Passport Covers & Wallets', 'Wedding Accessories', 'Fashionable Canes'],
    'Luggage & Bags': ['Woman\'s Handbags', 'Waist Packs', 'Unisex Backpacks', 'Men\'s Bags', 'Wallets & Holders', 'Special Purpose Bags', 'School Bags', 'Travel Bags', 'Luggage', 'Travel Accessories', 'Kids\' Bags', 'Bag Parts & Accessories'],
    'Sports & Entertainment': ['Cycling', 'Fishing', 'Boats', 'Camping & Hiking', 'Hunting', 'Sports', 'Fitness, Yoga, Body Building', 'Water Sports', 'Sportswear & Accessories', 'Sports Safety', 'Entertainment', 'Boxing & Martial Arts', 'Roller, Skateboard'],
    'Mother & Kids': ['Baby Strollers & Accessories', 'Baby Diaper & Toilet Training', 'Kids Accessories', 'Baby Souvenirs', 'Feeding', 'Baby Care', 'Activity & Gear', 'Bedding', 'Baby Sterilization & Appliances', 'Baby & Toddler Toys', 'Baby Food', 'Safety', 'Pregnancy & Maternity'],
    'Toys & Hobbies': ['Games & Puzzles', 'Arts, Crafts & Sewing', 'Remote Control Toys', 'Action & Toy Figures', 'Pools & Water Fun', 'Musical Instruments', 'Dolls & Accessories', 'Stuffed Animals & Plush', 'Play Vehicles & Models', 'Outdoor Fun & Sports', 'Learning & Education', 'Hobby & Collectibles', 'Calssic Toys', 'Stress Relief Toy', 'Pretend Play', 'Electronic Toys', 'Gags', 'Novelty & Gag Toys', 'Magic Tricks', 'Stamps Toys', 'Craft Toys'],
    'Beauty & Health': ['Health Care', 'Professional Equipment', 'Makeup', 'Nail Art & Tools', 'Massage & Relaxation', 'Hair Care & Styling', 'Oral Hygiene', 'Skin Care', 'Hair Extensions & Wigs', 'Skin Care Tools', 'Shaving & Hair Removal', 'Personal Care Devices', 'Fragrances & Deodorants', 'Bath & Shower', 'Personal Hygiene'],
    '18+': ['Sex Products', 'Clothing', 'Dakimakura', 'Adult Toys'],
    'Pet Products': ['Dog Supplies', 'Cat Supplies', 'Fish & Aquatic Pet Supplies', 'Farm Animal Supplies', 'Reptiles & Amphibians', 'Pet Medical Supplies', 'Small Animals', 'Bird Supplies', 'Pet Health Care & Hygiene', 'Insect Supplies', 'GPS Trackers', 'Pet Care Room', 'Pet Memorials', 'Pet Electronics', 'Pet Microchips', 'Pet Strollers', 'Pet Furniture'],
    'Food': ['Grocery', 'Coffee', 'Tea', 'Canned Food', 'Drinks', 'Fruit & Berries', 'Nut & Kernel', 'Ready Meal', 'Vegetables & Greens', 'Milk And Eggs', 'Dried Fruit', 'Bread & Pastries', 'Grain Products', 'Meat', 'Cheese', 'Frozen Products', 'Snacks']
  };

  // Получаем ссылки на DOM-элементы
  @ViewChild('catalogContainer') catalogContainer!: ElementRef;
  @ViewChild('langContainer') langContainer!: ElementRef;
  @ViewChild('menuContainer') menuContainer!: ElementRef;

  closeOtherContainers(except: string): void { // Закрывает все выпадающие окна, кроме указанного
    if (except !== 'catalog' && this.isCatalogActive) {
      this.isCatalogFadingOut = true;
      setTimeout(() => {
        this.isCatalogActive = false;
        this.isCatalogFadingOut = false;
      }, 400);
    }
    if (except !== 'lang' && this.isLangActive) {
      this.isLangFadingOut = true;
      setTimeout(() => {
        this.isLangActive = false;
        this.isLangFadingOut = false;
      }, 400);
    }
    if (except !== 'menu' && this.isMenuActive) {
      this.isMenuFadingOut = true;
      setTimeout(() => {
        this.isMenuActive = false;
        this.isMenuFadingOut = false;
      }, 400);
    }
  }

  toggleLanguageDropdown(): void { // Переключение отображения language-container
    this.closeOtherContainers('lang');
    if (this.isLangActive) {
      this.isLangFadingOut = true;
      setTimeout(() => {
        this.isLangActive = false;
        this.isLangFadingOut = false;
      }, 400);
    } else {
      this.isLangActive = true;
      this.isLangFadingOut = false;
    }
  }

  toggleMenuDropdown(): void { // Переключение отображения menu-container
    this.closeOtherContainers('menu');
    if (this.isMenuActive) {
      this.isMenuFadingOut = true;
      setTimeout(() => {
        this.isMenuActive = false;
        this.isMenuFadingOut = false;
        this.isCategoriesVisible = false; // Сбрасываем отображение категорий при закрытии меню
      }, 400);
    } else {
      this.isMenuActive = true;
      this.isMenuFadingOut = false;
    }
  }

  toggleCatalogDropdown(): void { // Переключение отображения catalog-container
    this.closeOtherContainers('catalog');
    if (this.isCatalogActive) {
      this.isCatalogFadingOut = true;
      setTimeout(() => {
        this.isCatalogActive = false;
        this.isCatalogFadingOut = false;
      }, 400);
    } else {
      this.isCatalogActive = true;
      this.isCatalogFadingOut = false;
      this.selectedCategory = 'Mobile phones';
    }
  }

  selectCategory(category: string): void { // Устанавливает выбранную категорию
    this.selectedCategory = category;
  }
  isSelected(category: string): boolean { // Проверяет, выбрана ли данная категория
    return this.selectedCategory === category;
  }
  getSelectedCategoryContent(): string[] { // Получает список подкатегорий выбранной категории
    return this.catalogContent[this.selectedCategory] || [];
  }
  toggleCategoriesView(): void { // Метод переключения отображения меню мобильной версии
    this.isCategoriesVisible = !this.isCategoriesVisible;
  }
}