CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';
--RECIPES
CREATE TABLE recipes(
  id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  title TINYTEXT NOT NULL,
  instructions TEXT,
  img TEXT NOT NULL,
  category ENUM(
    'breakfast',
    'lunch',
    'dinner',
    'snack',
    'dessert'
  ) NOT NULL,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);
DROP table recipes;
SELECT *
FROM recipes;
SELECT recipes.*,
  accounts.*
FROM recipes
  INNER JOIN accounts ON accounts.id = recipes.creator_id
WHERE recipes.id = LAST_INSERT_ID();
--INGREDIENTS
CREATE TABLE ingredients(
  id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name TINYTEXT NOT NULL,
  quantity TINYTEXT NOT NULL,
  recipe_id INT UNSIGNED NOT NULL,
  FOREIGN KEY (recipe_id) REFERENCES recipes(id) ON DELETE CASCADE
);
drop table ingredients;
SELECT *
FROM ingredients
WHERE recipe_id = 19;
--FAVORITES
CREATE TABLE favorites (
  id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  recipe_id INT UNSIGNED NOT NULL,
  account_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (recipe_id) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (account_id) REFERENCES accounts(id) ON DELETE CASCADE,
  UNIQUE(recipe_id, account_id)
);
SELECT *
FROM favorites;
SELECT *
FROM favorites
WHERE id = 1;
SELECT favorites.*,
  recipes.*,
  accounts.*
FROM favorites
  INNER JOIN recipes ON recipes.id = favorites.recipe_id
  INNER JOIN accounts ON accounts.id = recipes.creator_id
WHERE favorites.account_id = '67d3450505108bbebf54d9cb';