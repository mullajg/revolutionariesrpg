import React from "react";
import { Card, CardBody, Input, Table, TableHeader, TableColumn, TableBody, TableRow, TableCell, Button, Textarea, Chip } from "@heroui/react";
import { Icon } from "@iconify/react";

interface Weapon {
    id: string;
    name: string;
    attackBonus: number;
    damage: string;
    damageType: string;
    properties: string;
}

interface Action {
    id: string;
    name: string;
    description: string;
    type: "action" | "bonus" | "reaction";
}

export default function CombatSection(){
    const [weapons, setWeapons] = React.useState<Weapon[]>([
        { id: "1", name: "Longsword", attackBonus: 5, damage: "1d8+3", damageType: "Slashing", properties: "Versatile (1d10)" },
        { id: "2", name: "Handaxe", attackBonus: 5, damage: "1d6+3", damageType: "Slashing", properties: "Light, Thrown (20/60)" },
        { id: "3", name: "Longbow", attackBonus: 4, damage: "1d8+2", damageType: "Piercing", properties: "Ammunition, Two-handed" },
    ]);

    const [actions, setActions] = React.useState<Action[]>([
        { id: "1", name: "Second Wind", description: "Regain 1d10 + fighter level HP. Once per short rest.", type: "bonus" },
        { id: "2", name: "Action Surge", description: "Take an additional action. Once per short rest.", type: "action" },
        { id: "3", name: "Opportunity Attack", description: "When enemy leaves your reach, make a melee attack.", type: "reaction" },
    ]);

    const [newWeapon, setNewWeapon] = React.useState<Weapon>({
        id: "", name: "", attackBonus: 0, damage: "", damageType: "", properties: ""
    });

    const [newAction, setNewAction] = React.useState<Action>({
        id: "", name: "", description: "", type: "action"
    });

    const [armorClass, setArmorClass] = React.useState("16");
    const [initiative, setInitiative] = React.useState("+2");
    const [speed, setSpeed] = React.useState("30");

    const handleAddWeapon = () => {
        if (newWeapon.name) {
            setWeapons([...weapons, { ...newWeapon, id: Date.now().toString() }]);
            setNewWeapon({ id: "", name: "", attackBonus: 0, damage: "", damageType: "", properties: "" });
        }
    };

    const handleAddAction = () => {
        if (newAction.name) {
            setActions([...actions, { ...newAction, id: Date.now().toString() }]);
            setNewAction({ id: "", name: "", description: "", type: "action" });
        }
    };

    const handleRemoveWeapon = (id: string) => {
        setWeapons(weapons.filter(weapon => weapon.id !== id));
    };

    const handleRemoveAction = (id: string) => {
        setActions(actions.filter(action => action.id !== id));
    };

    return (
        <div className="space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <Card>
                    <CardBody className="flex flex-col items-center p-3">
                        <div className="flex items-center justify-center w-10 h-10 rounded-full bg-primary-100 dark:bg-primary-900/30 mb-2">
                            <Icon icon="lucide:shield" className="w-5 h-5 text-primary-500" />
                        </div>
                        <h3 className="text-sm font-semibold text-slate-700 dark:text-slate-300">Armor Class</h3>
                        <Input
                            type="text"
                            size="sm"
                            value={armorClass}
                            onValueChange={setArmorClass}
                            className="w-16 text-center mt-2"
                        />
                    </CardBody>
                </Card>
                <Card>
                    <CardBody className="flex flex-col items-center p-3">
                        <div className="flex items-center justify-center w-10 h-10 rounded-full bg-primary-100 dark:bg-primary-900/30 mb-2">
                            <Icon icon="lucide:zap" className="w-5 h-5 text-primary-500" />
                        </div>
                        <h3 className="text-sm font-semibold text-slate-700 dark:text-slate-300">Initiative</h3>
                        <Input
                            type="text"
                            size="sm"
                            value={initiative}
                            onValueChange={setInitiative}
                            className="w-16 text-center mt-2"
                        />
                    </CardBody>
                </Card>
                <Card>
                    <CardBody className="flex flex-col items-center p-3">
                        <div className="flex items-center justify-center w-10 h-10 rounded-full bg-primary-100 dark:bg-primary-900/30 mb-2">
                            <Icon icon="lucide:footprints" className="w-5 h-5 text-primary-500" />
                        </div>
                        <h3 className="text-sm font-semibold text-slate-700 dark:text-slate-300">Speed</h3>
                        <Input
                            type="text"
                            size="sm"
                            value={speed}
                            onValueChange={setSpeed}
                            className="w-16 text-center mt-2"
                        />
                    </CardBody>
                </Card>
            </div>

            <div>
                <h2 className="text-xl font-semibold mb-4 text-slate-800 dark:text-slate-200">Weapons</h2>
                <Table
                    removeWrapper
                    aria-label="Weapons table"
                    classNames={{
                        th: "bg-slate-100 dark:bg-slate-800 text-slate-600 dark:text-slate-300",
                    }}
                >
                    <TableHeader>
                        <TableColumn>NAME</TableColumn>
                        <TableColumn>ATK BONUS</TableColumn>
                        <TableColumn>DAMAGE</TableColumn>
                        <TableColumn>TYPE</TableColumn>
                        <TableColumn>PROPERTIES</TableColumn>
                        <TableColumn>ACTIONS</TableColumn>
                    </TableHeader>
                    <TableBody>
                        {/*{weapons.map((weapon) => (*/}
                        {/*    <TableRow key={weapon.id}>*/}
                        {/*        <TableCell>{weapon.name}</TableCell>*/}
                        {/*        <TableCell>+{weapon.attackBonus}</TableCell>*/}
                        {/*        <TableCell>{weapon.damage}</TableCell>*/}
                        {/*        <TableCell>{weapon.damageType}</TableCell>*/}
                        {/*        <TableCell>{weapon.properties}</TableCell>*/}
                        {/*        <TableCell>*/}
                        {/*            <Button*/}
                        {/*                isIconOnly*/}
                        {/*                size="sm"*/}
                        {/*                color="danger"*/}
                        {/*                variant="light"*/}
                        {/*                onPress={() => handleRemoveWeapon(weapon.id)}*/}
                        {/*            >*/}
                        {/*                <Icon icon="lucide:trash-2" className="w-4 h-4" />*/}
                        {/*            </Button>*/}
                        {/*        </TableCell>*/}
                        {/*    </TableRow>*/}
                        {/*))}*/}
                        <TableRow>
                            <TableCell>
                                <Input
                                    size="sm"
                                    placeholder="Weapon name"
                                    value={newWeapon.name}
                                    onValueChange={(value) => setNewWeapon({ ...newWeapon, name: value })}
                                />
                            </TableCell>
                            <TableCell>
                                <Input
                                    size="sm"
                                    type="number"
                                    placeholder="Bonus"
                                    value={newWeapon.attackBonus.toString()}
                                    onValueChange={(value) => setNewWeapon({ ...newWeapon, attackBonus: parseInt(value) || 0 })}
                                />
                            </TableCell>
                            <TableCell>
                                <Input
                                    size="sm"
                                    placeholder="1d8+3"
                                    value={newWeapon.damage}
                                    onValueChange={(value) => setNewWeapon({ ...newWeapon, damage: value })}
                                />
                            </TableCell>
                            <TableCell>
                                <Input
                                    size="sm"
                                    placeholder="Type"
                                    value={newWeapon.damageType}
                                    onValueChange={(value) => setNewWeapon({ ...newWeapon, damageType: value })}
                                />
                            </TableCell>
                            <TableCell>
                                <Input
                                    size="sm"
                                    placeholder="Properties"
                                    value={newWeapon.properties}
                                    onValueChange={(value) => setNewWeapon({ ...newWeapon, properties: value })}
                                />
                            </TableCell>
                            <TableCell>
                                <Button
                                    size="sm"
                                    color="primary"
                                    onPress={handleAddWeapon}
                                >
                                    Add
                                </Button>
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </div>

            <div>
                <h2 className="text-xl font-semibold mb-4 text-slate-800 dark:text-slate-200">Actions</h2>
                <div className="space-y-4">
                    {actions.map((action) => (
                        <Card key={action.id} className="relative">
                            <CardBody className="p-4">
                                <div className="flex justify-between items-start">
                                    <div>
                                        <div className="flex items-center gap-2 mb-2">
                                            <h3 className="text-lg font-medium">{action.name}</h3>
                                            <Chip
                                                size="sm"
                                                color={action.type === "action" ? "primary" : action.type === "bonus" ? "secondary" : "default"}
                                            >
                                                {action.type === "action" ? "Action" : action.type === "bonus" ? "Bonus Action" : "Reaction"}
                                            </Chip>
                                        </div>
                                        <p className="text-sm text-slate-600 dark:text-slate-300">{action.description}</p>
                                    </div>
                                    <Button
                                        isIconOnly
                                        size="sm"
                                        color="danger"
                                        variant="light"
                                        onPress={() => handleRemoveAction(action.id)}
                                    >
                                        <Icon icon="lucide:trash-2" className="w-4 h-4" />
                                    </Button>
                                </div>
                            </CardBody>
                        </Card>
                    ))}

                    <Card>
                        <CardBody className="p-4">
                            <div className="space-y-3">
                                <Input
                                    label="Action Name"
                                    placeholder="Enter action name"
                                    value={newAction.name}
                                    onValueChange={(value) => setNewAction({ ...newAction, name: value })}
                                />
                                <Textarea
                                    label="Description"
                                    placeholder="Enter action description"
                                    value={newAction.description}
                                    onValueChange={(value) => setNewAction({ ...newAction, description: value })}
                                />
                                <div className="flex gap-2">
                                    <Button
                                        size="sm"
                                        color={newAction.type === "action" ? "primary" : "default"}
                                        variant={newAction.type === "action" ? "solid" : "bordered"}
                                        onPress={() => setNewAction({ ...newAction, type: "action" })}
                                    >
                                        Action
                                    </Button>
                                    <Button
                                        size="sm"
                                        color={newAction.type === "bonus" ? "secondary" : "default"}
                                        variant={newAction.type === "bonus" ? "solid" : "bordered"}
                                        onPress={() => setNewAction({ ...newAction, type: "bonus" })}
                                    >
                                        Bonus Action
                                    </Button>
                                    <Button
                                        size="sm"
                                        color={newAction.type === "reaction" ? "default" : "default"}
                                        variant={newAction.type === "reaction" ? "solid" : "bordered"}
                                        onPress={() => setNewAction({ ...newAction, type: "reaction" })}
                                    >
                                        Reaction
                                    </Button>
                                </div>
                                <div className="flex justify-end">
                                    <Button
                                        color="primary"
                                        onPress={handleAddAction}
                                    >
                                        Add Action
                                    </Button>
                                </div>
                            </div>
                        </CardBody>
                    </Card>
                </div>
            </div>
        </div>
    );
}