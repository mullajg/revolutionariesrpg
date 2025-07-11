import { Table, TableHeader, TableColumn, TableBody, TableRow, TableCell } from "@heroui/table";

export default function CompendiumTable({ columns, data }: { columns: string[]; data: any[] }) {
    return (
        <div className="overflow-x-auto">
        <Table>
            <TableHeader>
                {columns.map((column, index) => (
                    <TableColumn key={index} className="font-bold">
                        {column}
                    </TableColumn>
                ))}
            </TableHeader>
            <TableBody>
                {data.map((row, rowIndex) => (
                    <TableRow key={rowIndex}>
                        {columns.map((column, colIndex) => (
                            <TableCell key={colIndex}>{row[column]}</TableCell>
                        ))}
                    </TableRow>
                ))}
            </TableBody>
            </Table>
        </div>
    );
}