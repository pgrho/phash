using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash.Data
{
    public static class SqlFunctionFactory
    {
        private const string DEFAULT_FUNCTION_NAME = "dbo.GetCrossCorrelation";

        public static string GetCreateFunctionSql(string functionName = DEFAULT_FUNCTION_NAME)
            => $"CREATE FUNCTION {functionName}\r\n"
                + "(\r\n"
                + "    @x AS VARBINARY(4000),\r\n"
                + "    @y AS VARBINARY(4000)\r\n"
                + ")\r\n"
                + "RETURNS FLOAT\r\n"
                + "BEGIN\r\n"
                + "    DECLARE @n INT, @i INT, @d INT;\r\n"
                + "    SET @n = LEN(@x);\r\n"
                + "    DECLARE @sumx FLOAT = 0;\r\n"
                + "    DECLARE @sumy FLOAT = 0;\r\n"
                + "    SET @i = 1;\r\n"
                + "    WHILE @i <= @n\r\n"
                + "    BEGIN\r\n"
                + "        SET @sumx = @sumx + CAST(SUBSTRING(@x, @i, 1) AS TINYINT);\r\n"
                + "        SET @sumy = @sumy + CAST(SUBSTRING(@y, @i, 1) AS TINYINT);\r\n"
                + "        SET @i = @i + 1;\r\n"
                + "    END\r\n"
                + "    DECLARE @meanx FLOAT = @sumx / @n;\r\n"
                + "    DECLARE @meany FLOAT = @sumy / @n;\r\n"
                + "    DECLARE @max FLOAT = 0;\r\n"
                + "     SET @d = 1;\r\n"
                + "    WHILE @d <= @n\r\n"
                + "    BEGIN\r\n"
                + "        DECLARE @num FLOAT = 0;\r\n"
                + "        DECLARE @denx FLOAT = 0;\r\n"
                + "        DECLARE @deny FLOAT = 0;\r\n"
                + "        SET @i = 1;\r\n"
                + "        WHILE @i <= @n\r\n"
                + "        BEGIN\r\n"
                + "            DECLARE @vx FLOAT, @vy FLOAT\r\n"
                + "            SET @vx = CAST(SUBSTRING(@x, @i, 1) AS TINYINT) - @meanx;\r\n"
                + "            SET @vy = CAST(SUBSTRING(@y, 1 + (@n + @i - @d) % @n, 1) AS TINYINT) - @meany;\r\n"
                + "            SET @num = @num + @vx * @vy;\r\n"
                + "            SET @denx = @denx + @vx * @vx;\r\n"
                + "            SET @deny = @deny + @vy * @vy;\r\n"
                + "            SET @i = @i + 1;\r\n"
                + "        END\r\n"
                + "        DECLARE @r FLOAT;\r\n"
                + "        SET @r = IIF(@denx * @deny = 0, 1, @num / SQRT(@denx * @deny));\r\n"
                + "        SET @max = IIF(@r > @max, @r, @max);\r\n"
                + "        SET @d = @d + 1;\r\n"
                + "    END\r\n"
                + "    RETURN @max;\r\n"
                + "END";

        public static string GetDropFunctionSql(string functionName = DEFAULT_FUNCTION_NAME)
            => $"DROP FUNCTION {functionName}";

        public static string GetDropIfExistsFunctionSql(string functionName = DEFAULT_FUNCTION_NAME)
            => $"IF OBJECT_ID('{functionName}', 'U') IS NOT NULL\r\n"
            + $"    DROP FUNCTION {functionName}";
    }
}