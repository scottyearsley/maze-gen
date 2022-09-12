using System.Text;
using System.IO;

namespace MazeGen
{
    public class HtmlMazeRenderer : IMazeRenderer
    {
        public void Render(Maze maze)
        {
            
            var sb = new StringBuilder();

            sb.AppendLine("<html>");
            RenderStyle(sb);
            sb.AppendLine("<body>");
            sb.AppendLine($"<h1>Random Maze - {maze.Width} x {maze.Height}</h1>");
            sb.AppendLine("<table>");

            for (int rowIndex = 0; rowIndex < maze.Height; rowIndex++)
            {
                sb.AppendLine("<tr>");

                for (int columnIndex = 0; columnIndex < maze.Height; columnIndex++)
                {
                    var cellContent = "&nbsp;"; //cell.VisitIndex
                    var cell = maze.GetCellAt(rowIndex, columnIndex);
                    var cellStyle = CalculateCellClasses(cell);
                    sb.AppendLine($"<td class=\"{cellStyle}\">{cellContent}</td>");
                }

                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body></html>");

            File.WriteAllText("c:\\temp\\maze.html", sb.ToString());
        }

        private string CalculateCellClasses(Cell cell)
        {
            string cellStyle = "";

            if (cell.HasWall(WallPosition.Left))
            {
                cellStyle += "wall-left ";
            }
            if (cell.HasWall(WallPosition.Right))
            {
                cellStyle += "wall-right ";
            }
            if (cell.HasWall(WallPosition.Top))
            {
                cellStyle += "wall-top ";
            }
            if (cell.HasWall(WallPosition.Bottom))
            {
                cellStyle += "wall-bottom ";
            }

            return cellStyle;
        }

        private void RenderStyle(StringBuilder sb)
        {
            sb.AppendLine("<style>");
            sb.AppendLine("table { border-collapse: collapse; } ");
            sb.AppendLine("td { width: 20px; height:20px; font-family: \"Lucida Console\", Courier, monospace; font-size:10px; } ");
            sb.AppendLine(".wall-left { border-left: 2px solid black; } ");
            sb.AppendLine(".wall-right { border-right: 2px solid black; } ");
            sb.AppendLine(".wall-top { border-top: 2px solid black; } ");
            sb.AppendLine(".wall-bottom { border-bottom: 2px solid black; } ");

            sb.AppendLine("</style>");
        }
    }
}