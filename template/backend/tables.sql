-- tabela de usuários
CREATE TABLE "Users" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "Username" VARCHAR(255) NOT NULL,  -- Nome de usuário
    "Email" VARCHAR(255) UNIQUE NOT NULL,  -- Email único
    "Phone" VARCHAR(20),  -- Número de telefone
    "Password" VARCHAR(255) NOT NULL,  -- Senha (hash)
    "Role" VARCHAR(50) NOT NULL,  -- Papel do usuário (admin, user, etc.)
    "Status" VARCHAR(50) NOT NULL,  -- Status do usuário (active, inactive, suspended)
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  -- Data de criação
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Data de atualização
);

-- Índices adicionais para melhorar o desempenho nas consultas
CREATE INDEX idx_users_email ON "Users"("Email");
CREATE INDEX idx_users_username ON "Users"("Username");

-- tabela de Vendas
CREATE TABLE "Sales" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "SaleNumber" VARCHAR(50) NOT NULL,                -- Numero da Nota
    "SaleDate" DATE NOT NULL,                         -- Data da Venda
    "Customer" VARCHAR(255) NOT NULL,                 -- Id do Usuario
    "TotalSaleAmount" DECIMAL(10, 2) NOT NULL,        -- Total da Nota
	"TotalItems" integer NOT NULL,        			  -- Total de Itens da Nota
    "Branch" VARCHAR(255) NOT NULL,                   -- Nome da Filial que fez a venda
    "Cancelled" BOOLEAN NOT NULL DEFAULT FALSE,        -- Se a venda foi cancelada
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  -- Data de criação
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  -- Data de atualização
	"CancelledAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Data de criação
);

-- Tabela de Produtos
CREATE TABLE "Products" (
    "Id"  UUID PRIMARY KEY DEFAULT gen_random_uuid(),                  -- Identificador único do produto
    "ProductName" VARCHAR(255) NOT NULL,              -- Nome do produto	
	"Description" VARCHAR(255) NOT NULL,              -- Descricao do produto	
	"Category" VARCHAR(255) NOT NULL,              -- Descricao do produto	
	"Image" VARCHAR(255) ,              -- Descricao do produto		
    "Price" DECIMAL(10, 2) NOT NULL,               -- Preço unitário do produto
	"CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  -- Data de criação
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Data de atualização
);


-- Tabela de Produtos na Venda (relaciona os produtos com a venda)
CREATE TABLE "SalesProducts" (
    "Id" UUID DEFAULT gen_random_uuid(),
    "SaleNumber" UUID NOT NULL,                       -- Identificador da venda
    "ProductId" UUID NOT NULL,                        -- Identificador do produto
    "Quantity" INT NOT NULL,                          -- Quantidade do produto na venda
    "TotalItemAmount" DECIMAL(10, 2) NOT NULL,        -- Valor total do item (quantidade * preço unitário - desconto)
    "Discount" DECIMAL(10, 2) DEFAULT 0.00,           -- Desconto aplicado ao produto
    CONSTRAINT "PK_SalesProducts" PRIMARY KEY ("Id"),  
    CONSTRAINT "FK_SalesProducts_Sales" FOREIGN KEY ("SaleNumber") REFERENCES "Sales"("Id") ON DELETE CASCADE, -- Chave estrangeira para Sales
    CONSTRAINT "FK_SalesProducts_Products" FOREIGN KEY ("ProductId") REFERENCES "Products"("Id") ON DELETE CASCADE -- Chave estrangeira para Products
);



