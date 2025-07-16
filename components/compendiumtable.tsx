import { Table, TableHeader, TableColumn, TableBody, TableRow, TableCell } from "@heroui/table";
import { Modal, ModalContent, ModalHeader, ModalBody, ModalFooter, useDisclosure } from "@heroui/modal"
import { Button } from "@heroui/button";
import { Input } from "@heroui/input";
import { useState, useMemo, useEffect } from "react";
import { CiCirclePlus } from "react-icons/ci";
import { CiSearch } from "react-icons/ci";
import { FaSort } from "react-icons/fa";

export default function CompendiumTable({ columns, displayColumns, data, detailText }: { columns: string[]; displayColumns: string[];  data: any[], detailText: string }) {
    const { isOpen, onOpen, onOpenChange } = useDisclosure();
    const [selectedRowData, setSelectedRowData] = useState<number | null>(null); // Track selected row index
    const [searchQuery, setSearchQuery] = useState("");
    const [sort, setSort] = useState({ keyToSort: "name", direction: "asc" });
    const [filter, setFilter] = useState("");
    const [isMobile, setIsMobile] = useState<boolean>(false);

    useEffect(() => {
        const checkMobile = () => {
            setIsMobile(window.innerWidth < 768);
        };
        checkMobile();
        window.addEventListener('resize', checkMobile);
        return () => {
            window.removeEventListener('resize', checkMobile);
        };
    }, [])

    const handleRowClick = (rowIndex: number) => {
        setSelectedRowData(data[rowIndex]);
        onOpen();
    };

    function handleHeaderClick(index: number) {
        const columnsCurrentlyDisplayed = isMobile ? columns.slice(0, 2) : columns;
        const columnKey = columnsCurrentlyDisplayed[index];
        setSort({
            keyToSort: columnKey,
            direction:
                columnKey === sort.keyToSort
                    ? sort.direction === "asc"
                        ? "desc"
                        : "asc"
                    : "desc"
        });
    }

    function getSortedArray(arrayToSort: any[]) {
        if (sort.direction === "asc") {
            return arrayToSort.sort((a, b) => (a[sort.keyToSort] > b[sort.keyToSort] ? 1 : -1));
        }
        return arrayToSort.sort((a, b) => (a[sort.keyToSort] > b[sort.keyToSort] ? -1 : 1));
    }

    const topContent = useMemo(() => {
        return (
            <div className="flex flex-col gap-4">
                <div className="flex justify-between gap-3 items-end">
                    <Input
                        className="w-full sm:max-w-[44%]"
                        placeholder="Search..."
                        startContent={<CiSearch />}
                        onChange={(e) => setFilter(e.target.value)                        }
                    />
                    <div className="flex gap-3">
                        <Button color="primary" endContent={<CiCirclePlus size="1.2rem" />}>
                            Add New
                        </Button>
                    </div>
                </div>
            </div>
        );
    }, [
        filter
    ]);

    // Memoize the filtered and sorted data to prevent unnecessary re-renders
    const filteredAndSortedData = useMemo(() => {
        const sortedData = getSortedArray(data);
        if (!filter) {
            return sortedData; // If no filter, return sorted data directly
        }

        const lowerCaseFilter = filter.toLowerCase();
        return sortedData.filter((item: any) => {
            // Check if the filter matches any column's value
            const columnsToCheck = isMobile ? columns.slice(0, 2) : columns;
            return columnsToCheck.some(column =>
                String(item[column]).toLowerCase().includes(lowerCaseFilter)
            );
        });
    }, [data, sort, filter, columns, isMobile]); // Add 'columns' to dependencies

    const columnsInUse = isMobile ? columns.slice(0, 2) : columns;
    const displayColumnsInUse = isMobile ? displayColumns.slice(0, 2) : displayColumns;

    return (
        <>
        <div className="overflow-x-auto">
        <Table
            isStriped
            selectionMode="single"
            topContent={topContent}
                >
            <TableHeader>
                {displayColumnsInUse.map((displayColumn, index) => (
                    <TableColumn key={index} className="font-bold" onClick={() => handleHeaderClick(index)}>
                        {displayColumn}
                        <FaSort></FaSort>
                    </TableColumn>
                ))}
            </TableHeader>
                <TableBody>
                    {filteredAndSortedData.map((row: any, rowIndex: number) => (
                        <TableRow
                            key={rowIndex}
                            onClick={() => handleRowClick(rowIndex)}
                            className="cursor-pointer">
                            {columnsInUse.map((column, colIndex) => (
                                <TableCell key={colIndex}>{row[column]}</TableCell>
                            ))}
                        </TableRow>
                ))}
            </TableBody>
                    </Table>
            </div>
            <Modal
                isOpen={isOpen}
                onOpenChange={onOpenChange}
                // Optional: You can customize modal behavior here, e.g., backdrop, scroll behavior
                scrollBehavior="inside" // Example: make content scrollable if it's long
            >
                <ModalContent>
                    {(onClose) => (
                        <>
                            <ModalHeader className="flex flex-col gap-1">
                                {selectedRowData ? `${(selectedRowData as any)[columns[0]] || 'Selected Item'}` : 'Item Details'}
                            </ModalHeader>
                            <ModalBody>
                                {selectedRowData ? (
                                    <div>
                                        {columns.map((column, index) => (
                                            <p key={index}>
                                                <strong>{displayColumns[index]}: {(selectedRowData as any)[columns[index]]}</strong>
                                            </p>
                                        ))}
                                        <br/>
                                        <p>{(selectedRowData as any)[detailText]}</p>
                                    </div>
                                ) : (
                                    <p>No data selected.</p>
                                )}
                            </ModalBody>
                            <ModalFooter>
                                <Button color="danger" variant="light" onPress={onClose}>
                                    Close
                                </Button>
                            </ModalFooter>
                        </>
                    )}
                </ModalContent>
            </Modal>
            </>
    );
}