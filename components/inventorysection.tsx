import React from "react";
import { Card, CardBody, Input, Table, TableHeader, TableColumn, TableBody, TableRow, TableCell, Button, Textarea, Chip } from "@heroui/react";
import { Icon } from "@iconify/react";

interface Item {
    id: string;
    name: string;
    quantity: number;
    weight: number;
    value: string;
    description: string;
    category: "weapon" | "armor" | "gear" | "consumable" | "treasure" | "other";
}

export default function InventorySection(){
    const [items, setItems] = React.useState<Item[]>([
        { id: "1", name: "Backpack", quantity: 1, weight: 5, value: "2 gp", description: "A leather backpack with multiple compartments.", category: "gear" },
        { id: "2", name: "Rations (1 day)", quantity: 10, weight: 2, value: "5 sp", description: "Dried meat, fruit, and hardtack sufficient to sustain one person for one day.", category: "consumable" },
        { id: "3", name: "Rope, hempen (50 feet)", quantity: 1, weight: 10, value: "1 gp", description: "Has 2 hit points and can be burst with a DC 17 Strength check.", category: "gear" },
        { id: "4", name: "Potion of Healing", quantity: 3, weight: 0.5, value: "50 gp", description: "Regains 2d4+2 hit points when drunk.", category: "consumable" },
        { id: "5", name: "Gold Coin", quantity: 75, weight: 0.02, value: "1 gp", description: "Standard currency.", category: "treasure" },
    ]);

    const [newItem, setNewItem] = React.useState<Item>({
        id: "", name: "", quantity: 1, weight: 0, value: "", description: "", category: "gear"
    });

    const [expandedItemId, setExpandedItemId] = React.useState<string | null>(null);
    const [showAddForm, setShowAddForm] = React.useState(false);
    const [carryingCapacity, setCarryingCapacity] = React.useState("240");
    const [currency, setCurrency] = React.useState({
        pp: "0",
        gp: "75",
        ep: "0",
        sp: "30",
        cp: "15"
    });

    const totalWeight = React.useMemo(() => {
        return items.reduce((sum, item) => sum + (item.weight * item.quantity), 0).toFixed(1);
    }, [items]);

    const handleAddItem = () => {
        if (newItem.name) {
            setItems([...items, { ...newItem, id: Date.now().toString() }]);
            setNewItem({
                id: "", name: "", quantity: 1, weight: 0, value: "", description: "", category: "gear"
            });
            setShowAddForm(false);
        }
    };

    const handleRemoveItem = (id: string) => {
        setItems(items.filter(item => item.id !== id));
    };

    const handleQuantityChange = (id: string, change: number) => {
        setItems(items.map(item => {
            if (item.id === id) {
                const newQuantity = Math.max(0, item.quantity + change);
                return { ...item, quantity: newQuantity };
            }
            return item;
        }));
    };

    const toggleExpandItem = (id: string) => {
        setExpandedItemId(expandedItemId === id ? null : id);
    };

    const handleCurrencyChange = (type: keyof typeof currency, value: string) => {
        setCurrency({ ...currency, [type]: value });
    };

    const getCategoryIcon = (category: Item["category"]) => {
        switch (category) {
            case "weapon": return "lucide:sword";
            case "armor": return "lucide:shield";
            case "gear": return "lucide:backpack";
            case "consumable": return "lucide:potion";
            case "treasure": return "lucide:gem";
            default: return "lucide:package";
        }
    };

    const getCategoryColor = (category: Item["category"]) => {
        switch (category) {
            case "weapon": return "danger";
            case "armor": return "primary";
            case "gear": return "default";
            case "consumable": return "success";
            case "treasure": return "warning";
            default: return "default";
        }
    };

    return (
        <div className="space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Card>
                    <CardBody className="p-4">
                        <h3 className="text-lg font-semibold mb-3 text-slate-800 dark:text-slate-200">Weight</h3>
                        <div className="flex items-center gap-4">
                            <div className="flex-1">
                                <div className="flex justify-between text-sm mb-1">
                                    <span>Current: {totalWeight} lbs</span>
                                    <span>Capacity: {carryingCapacity} lbs</span>
                                </div>
                                <Progress
                                    value={parseFloat(totalWeight)}
                                    maxValue={parseFloat(carryingCapacity)}
                                    color={parseFloat(totalWeight) > parseFloat(carryingCapacity) * 0.8 ? "warning" : "primary"}
                                    aria-label="Weight"
                                />
                            </div>
                            <Input
                                type="text"
                                size="sm"
                                label="Capacity"
                                value={carryingCapacity}
                                onValueChange={setCarryingCapacity}
                                className="w-24"
                            />
                        </div>
                    </CardBody>
                </Card>

                <Card>
                    <CardBody className="p-4">
                        <h3 className="text-lg font-semibold mb-3 text-slate-800 dark:text-slate-200">Currency</h3>
                        <div className="grid grid-cols-5 gap-2">
                            <Input
                                type="text"
                                size="sm"
                                label="PP"
                                value={currency.pp}
                                onValueChange={(value) => handleCurrencyChange("pp", value)}
                            />
                            <Input
                                type="text"
                                size="sm"
                                label="GP"
                                value={currency.gp}
                                onValueChange={(value) => handleCurrencyChange("gp", value)}
                            />
                            <Input
                                type="text"
                                size="sm"
                                label="EP"
                                value={currency.ep}
                                onValueChange={(value) => handleCurrencyChange("ep", value)}
                            />
                            <Input
                                type="text"
                                size="sm"
                                label="SP"
                                value={currency.sp}
                                onValueChange={(value) => handleCurrencyChange("sp", value)}
                            />
                            <Input
                                type="text"
                                size="sm"
                                label="CP"
                                value={currency.cp}
                                onValueChange={(value) => handleCurrencyChange("cp", value)}
                            />
                        </div>
                    </CardBody>
                </Card>
            </div>

            <div className="flex justify-between items-center">
                <h2 className="text-xl font-semibold text-slate-800 dark:text-slate-200">Inventory</h2>
                <Button
                    color="primary"
                    onPress={() => setShowAddForm(!showAddForm)}
                    startContent={<Icon icon={showAddForm ? "lucide:minus" : "lucide:plus"} />}
                >
                    {showAddForm ? "Cancel" : "Add Item"}
                </Button>
            </div>

            {showAddForm && (
                <Card>
                    <CardBody className="p-4">
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                            <Input
                                label="Item Name"
                                placeholder="Enter item name"
                                value={newItem.name}
                                onValueChange={(value) => setNewItem({ ...newItem, name: value })}
                            />
                            <div className="grid grid-cols-2 gap-2">
                                <Input
                                    type="number"
                                    label="Quantity"
                                    placeholder="1"
                                    value={newItem.quantity.toString()}
                                    onValueChange={(value) => setNewItem({ ...newItem, quantity: parseInt(value) || 0 })}
                                />
                                <Input
                                    type="number"
                                    label="Weight (lbs)"
                                    placeholder="0"
                                    value={newItem.weight.toString()}
                                    onValueChange={(value) => setNewItem({ ...newItem, weight: parseFloat(value) || 0 })}
                                />
                            </div>
                            <Input
                                label="Value"
                                placeholder="5 gp"
                                value={newItem.value}
                                onValueChange={(value) => setNewItem({ ...newItem, value: value })}
                            />
                            <Select
                                label="Category"
                                placeholder="Select category"
                                selectedKeys={[newItem.category]}
                            >
                                <SelectItem key="weapon" value="weapon">Weapon</SelectItem>
                                <SelectItem key="armor" value="armor">Armor</SelectItem>
                                <SelectItem key="gear" value="gear">Gear</SelectItem>
                                <SelectItem key="consumable" value="consumable">Consumable</SelectItem>
                                <SelectItem key="treasure" value="treasure">Treasure</SelectItem>
                                <SelectItem key="other" value="other">Other</SelectItem>
                            </Select>
                        </div>
                        <Textarea
                            label="Description"
                            placeholder="Enter item description"
                            value={newItem.description}
                            onValueChange={(value) => setNewItem({ ...newItem, description: value })}
                            className="mt-4"
                        />
                        <div className="flex justify-end mt-4">
                            <Button
                                color="primary"
                                onPress={handleAddItem}
                            >
                                Add Item
                            </Button>
                        </div>
                    </CardBody>
                </Card>
            )}

            <div className="space-y-4">
                {items.map((item) => (
                    <Card key={item.id} className="overflow-hidden">
                        <CardBody className="p-0">
                            <div
                                className="p-4 flex justify-between items-center cursor-pointer"
                                onClick={() => toggleExpandItem(item.id)}
                            >
                                <div className="flex items-center gap-3">
                                    <div className="flex items-center justify-center w-8 h-8 rounded-full bg-slate-100 dark:bg-slate-800">
                                        <Icon icon={getCategoryIcon(item.category)} className={`w-4 h-4 text-${getCategoryColor(item.category)}-500`} />
                                    </div>
                                    <div>
                                        <h3 className="text-lg font-medium">{item.name}</h3>
                                        <div className="flex items-center gap-2 text-xs text-slate-500 dark:text-slate-400">
                                            <span>{item.value}</span>
                                            <span>-</span>
                                            <span>{item.weight} lbs each</span>
                                        </div>
                                    </div>
                                </div>
                                <div className="flex items-center gap-2">
                                    <div className="flex items-center">
                                        <Button
                                            isIconOnly
                                            size="sm"
                                            variant="light"
                                        >
                                            <Icon icon="lucide:minus" className="w-4 h-4" />
                                        </Button>
                                        <span className="mx-2 min-w-[30px] text-center">{item.quantity}</span>
                                        <Button
                                            isIconOnly
                                            size="sm"
                                            variant="light"
                                        >
                                            <Icon icon="lucide:plus" className="w-4 h-4" />
                                        </Button>
                                    </div>
                                    <Button
                                        isIconOnly
                                        size="sm"
                                        color="danger"
                                        variant="light"
                                    >
                                        <Icon icon="lucide:trash-2" className="w-4 h-4" />
                                    </Button>
                                    <Icon
                                        icon={expandedItemId === item.id ? "lucide:chevron-up" : "lucide:chevron-down"}
                                        className="w-5 h-5 text-slate-400"
                                    />
                                </div>
                            </div>
                            {expandedItemId === item.id && item.description && (
                                <div className="p-4 pt-0 border-t border-slate-200 dark:border-slate-700">
                                    <p className="text-slate-600 dark:text-slate-300 text-sm">{item.description}</p>
                                </div>
                            )}
                        </CardBody>
                    </Card>
                ))}
            </div>
        </div>
    );
}

// Import needed for the Progress component
const Progress = ({ value, maxValue, color, className, ...props }: {
    value: number,
    maxValue: number,
    color: "primary" | "warning" | "danger" | "success",
    className?: string,
    [key: string]: any
}) => {
    const percentage = Math.min(100, (value / maxValue) * 100);

    return (
        <div className={`w-full bg-slate-200 dark:bg-slate-700 rounded-full h-2 ${className || ""}`} {...props}>
            <div
                className={`bg-${color}-500 h-2 rounded-full`}
                style={{ width: `${percentage}%` }}
            />
        </div>
    );
};

// Import needed for the Select component
const Select = ({ children, label, ...props }: {
    children: React.ReactNode,
    label?: string,
    [key: string]: any
}) => {
    return (
        <div className="flex flex-col gap-1.5">
            {label && <label className="text-sm font-medium text-slate-700 dark:text-slate-300">{label}</label>}
            <select
                className="bg-transparent border border-slate-300 dark:border-slate-600 rounded-md px-3 py-1.5 text-sm"
                {...props}
            >
                {children}
            </select>
        </div>
    );
};

// Import needed for the SelectItem component
const SelectItem = ({ children, ...props }: {
    children: React.ReactNode,
    [key: string]: any
}) => {
    return (
        <option {...props}>{children}</option>
    );
};