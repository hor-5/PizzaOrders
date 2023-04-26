using SistemaPizzeria.Entidades;

namespace SistemaPizzeria.Datastore
{
    public class DataStore
    {
        DBOperation dbOperation = new DBOperation();

        //PIZZA
        public List<Pizza> getPizzas()
        {
            List<Pizza> pizzas = new List<Pizza>();
            string sql = "SELECT IdPizza, Name, IsGlutenFree, Size, Price FROM Pizzas";
            pizzas = dbOperation.OperationQuery<Pizza>(sql);
            return pizzas;

        }

        public Pizza getPizzaById(int IdPizza)
        {
            Pizza pizza = new Pizza();
            string sql = "SELECT IdPizza, Name, IsGlutenFree, Size, Price FROM Pizzas WHERE IdPizza = @IdPizza";
            pizza = dbOperation.OperationQueryById<Pizza>(sql, new {IdPizza = IdPizza });
            return pizza;
        }

        public int insertPizza(Pizza pizza)
        {
            string sql = "INSERT INTO Pizzas ( Name, IsGlutenFree, Size, Price) OUTPUT INSERTED.IdPizza  VALUES"
                         + "(@Name, @IsGlutenFree, @Size,@Price)";

            Object paramList = new
            {
                Name = pizza.Name,
                IsGlutenFree = pizza.IsGlutenFree,
                Size = pizza.Size,
                Price = pizza.Price
            };

            int idInserted = dbOperation.OperationExecuteWithIdentity(sql, paramList);

            return idInserted;
        }

        public int updatePizza(Pizza pizza)
        {
            string sql = "UPDATE Pizzas SET Name = @Name,IsGlutenFree=@IsGlutenFree, Size=@Size, Price=@Price"
                        +"  WHERE idPizza = @IdPizza";
            Object paramList = new
            {
                IdPizza = pizza.IdPizza,
                Name = pizza.Name,
                IsGlutenFree = pizza.IsGlutenFree,
                Size = pizza.Size,
                Price = pizza.Price
            };
            int affectedRows = dbOperation.OperationExecute(sql, paramList);

            return affectedRows;

        }

        public int deletePizza(int IdPizza)
        {
            string sql = "DELETE FROM Pizzas WHERE IdPizza =@IdPizza";
            Object paramList = new { IdPizza = IdPizza };
            int affectedRows = dbOperation.OperationExecute(sql, paramList);
            return affectedRows;
        }

        //TOPPING
        public List<Topping> getToppings()
        {
            List <Topping> toppings = new List<Topping>();
            string sqlQuery = "SELECT IdTopping, Name, IsGlutenFree, Price FROM Toppings";
            toppings = dbOperation.OperationQuery<Topping>(sqlQuery);
            return toppings;
        }

        public Topping getToppingById(int idTopping)
        {
            Topping topping = new Topping();
            string sqlQuery = "SELECT IdTopping, Name, IsGlutenFree, Price FROM Toppings WHERE IdTopping = @idTopping";
            topping = dbOperation.OperationQueryById<Topping>(sqlQuery,new { idTopping = idTopping });
            return topping;
        }

        public int insertTopping(Topping topping)
        {
            string sql = "INSERT INTO Toppings ( Name, IsGlutenFree, Price) OUTPUT INSERTED.IdTopping  VALUES"
                         + "(@Name, @IsGlutenFree,@Price)";

            Object paramList = new
            {
                Name = topping.Name,
                IsGlutenFree = topping.IsGlutenFree,                
                Price = topping.Price
            };

            int idInserted = dbOperation.OperationExecuteWithIdentity(sql, paramList);

            return idInserted;
        }

        public int updateTopping(Topping topping)
        {
            string sql = "UPDATE Toppings SET Name = @Name,IsGlutenFree=@IsGlutenFree, Price=@Price"
                        + "  WHERE IdTopping = @IdTopping";
            Object paramList = new
            {
                IdTopping = topping.IdTopping,
                Name = topping.Name,
                IsGlutenFree = topping.IsGlutenFree,
                Price = topping.Price
            };
            int affectedRows = dbOperation.OperationExecute(sql, paramList);

            return affectedRows;

        }

        public int deleteTopping(int IdTopping)
        {
            string sql = "DELETE FROM Toppings WHERE IdTopping =@IdTopping";
            Object paramList = new { IdTopping = IdTopping };
            int affectedRows = dbOperation.OperationExecute(sql, paramList);
            return affectedRows;
        }

        //ORDEN
        private List<PizzaEncargada> getPizzasEncargadas(int idOrden)
        {
            List<PizzaEncargada> pizzasEncargadas = new List<PizzaEncargada>();
            string sqlQuery = "SELECT P.* FROM PizzasEncargadas PE INNER JOIN Ordenes O ON O.IdOrden = PE.fk_IdOrden " 
                +$"INNER JOIN Pizzas P ON P.IdPizza = PE.fk_IdPizza WHERE PE.fk_IdOrden = {idOrden}";

            pizzasEncargadas = dbOperation.OperationQuery<PizzaEncargada>(sqlQuery);

            
            foreach(PizzaEncargada pizza in pizzasEncargadas)
            {
                List<Topping> toppings = new List<Topping>();
                string sql = "SELECT T.* FROM PizzasEncargadas PE INNER JOIN ToppingsSeleccionados TS ON TS.fk_IdPizzaEncargada = PE.IdPizzaEncargada " 
                    +"INNER JOIN Toppings T ON T.IdTopping = TS.fk_IdTopping " 
                    +$"WHERE PE.fk_IdPizza = {pizza.IdPizza}";
                toppings = dbOperation.OperationQuery<Topping>(sql);

                pizza.toppingsSeleccionados = toppings;
            }

            return pizzasEncargadas;
        }

        public List<Orden> getAllOrders() {
            List<Orden> ordenes = new List<Orden>();
            List<int> ids = new List<int>();
            string query = "SELECT IdOrden FROM Ordenes";
            ids = dbOperation.OperationQuery<int>(query);
            foreach (int id in ids) {
                Orden orden = getOrdenById(id);
                ordenes.Add(orden);
            }
            return ordenes;
        }
        public Orden getOrdenById(int id)
        {
            Orden orden = new Orden();
            string sqlQuery = "SELECT IdOrden, Notes, IsCompleted,Total FROM Ordenes WHERE IdOrden = @idOrden";
            orden = dbOperation.OperationQueryById<Orden>(sqlQuery, new { idOrden =id});
            orden.pizzasEncargadas = getPizzasEncargadas(id);            

            return orden;

        }

        public Orden insertOrden(OrdenInsertDTO orden)
        {
            

            string sql = "INSERT INTO Ordenes ( Notes,IsCompleted, Total) OUTPUT INSERTED.IdOrden VALUES"
                         + "(@Notes, @IsCompleted,@Total)";


            Object paramList = new
            {
                Notes = orden.Notes,
                IsCompleted = false,
                Total = 0.0
            };

            int idOrdenInserted = dbOperation.OperationExecuteWithIdentity(sql, paramList);
            double Total = 0.0;

            string sqlQuery = "INSERT INTO PizzasEncargadas (fk_IdPizza, fk_IdOrden) OUTPUT INSERTED.IdPizzaEncargada VALUES (@IdPizza, @IdOrden)";
            
            foreach (PizzaEncargadaDTO pizza in orden.pizzasEncargadas) {

                int idPizzaInserted = dbOperation.OperationExecuteWithIdentity(sqlQuery, new { IdPizza = pizza.IdPizza, IdOrden = idOrdenInserted });
                //Obtener el precio para sumarlo al total

                string queryPrecioPizza = $"SELECT Price FROM Pizzas WHERE IdPizza = {pizza.IdPizza}";
                double precioPizza = dbOperation.OperationQuery<double>(queryPrecioPizza).First();
                Total += precioPizza;

                string toppingQuery = "INSERT INTO ToppingsSeleccionados (fk_IdTopping, fk_IdPizzaEncargada) VALUES (@IdTopping , @IdPizzaEncargada)";

                foreach (int idTopping in pizza.IdToppingsSeleccionados) {
                    int affectedRows = dbOperation.OperationExecute(toppingQuery, new { IdTopping = idTopping, IdPizzaEncargada = idPizzaInserted });
                    string queryPrecioTopping = $"SELECT Price FROM Toppings WHERE IdTopping = {idTopping}";
                    double precioTopping = dbOperation.OperationQuery<double>(queryPrecioTopping).First();
                    Total += precioTopping;
                }
            }

            string queryUpdateTotal = "UPDATE Ordenes SET Total= @Total WHERE IdOrden = @IdOrden";
            int affRows = dbOperation.OperationExecute(queryUpdateTotal, new { Total= Total, IdOrden= idOrdenInserted });

            return getOrdenById(idOrdenInserted);
            
        }

    }
}
