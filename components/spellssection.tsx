import React from "react";
import { Card, CardBody, Input, Table, TableHeader, TableColumn, TableBody, TableRow, TableCell, Button, Textarea, Chip, Select, SelectItem } from "@heroui/react";
import { Icon } from "@iconify/react";

interface Spell {
    id: string;
    name: string;
    level: number;
    castingTime: string;
    range: string;
    duration: string;
    description: string;
    school: string;
    prepared: boolean;
}

export default function SpellSection(){
    const [spells, setSpells] = React.useState<Spell[]>([
        {
            id: "1",
            name: "Fireball",
            level: 3,
            castingTime: "1 Action",
            range: "150 feet",
            duration: "Instantaneous",
            description: "A bright streak flashes from your pointing finger to a point you choose within range and then blossoms with a low roar into an explosion of flame. Each creature in a 20-foot-radius sphere centered on that point must make a Dexterity saving throw. A target takes 8d6 fire damage on a failed save, or half as much damage on a successful one.",
            school: "Evocation",
            prepared: true
        },
        {
            id: "2",
            name: "Shield",
            level: 1,
            castingTime: "1 Reaction",
            range: "Self",
            duration: "1 Round",
            description: "An invisible barrier of magical force appears and protects you. Until the start of your next turn, you have a +5 bonus to AC, including against the triggering attack, and you take no damage from magic missile.",
            school: "Abjuration",
            prepared: true
        },
        {
            id: "3",
            name: "Detect Magic",
            level: 1,
            castingTime: "1 Action",
            range: "Self",
            duration: "Concentration, up to 10 minutes",
            description: "For the duration, you sense the presence of magic within 30 feet of you. If you sense magic in this way, you can use your action to see a faint aura around any visible creature or object in the area that bears magic.",
            school: "Divination",
            prepared: false
        },
    ]);

    const [newSpell, setNewSpell] = React.useState<Spell>({
        id: "",
        name: "",
        level: 0,
        castingTime: "",
        range: "",
        duration: "",
        description: "",
        school: "",
        prepared: false
    });

    const [spellcastingAbility, setSpellcastingAbility] = React.useState("INT");
    const [spellSaveDC, setSpellSaveDC] = React.useState("15");
    const [spellAttackBonus, setSpellAttackBonus] = React.useState("+7");
    const [showAddForm, setShowAddForm] = React.useState(false);
    const [expandedSpellId, setExpandedSpellId] = React.useState<string | null>(null);

    const handleAddSpell = () => {
        if (newSpell.name) {
            setSpells([...spells, { ...newSpell, id: Date.now().toString() }]);
            setNewSpell({
                id: "",
                name: "",
                level: 0,
                castingTime: "",
                range: "",
                duration: "",
                description: "",
                school: "",
                prepared: false
            });
            setShowAddForm(false);
        }
    };

    const handleRemoveSpell = (id: string) => {
        setSpells(spells.filter(spell => spell.id !== id));
    };

    const togglePrepared = (id: string) => {
        setSpells(spells.map(spell =>
            spell.id === id ? { ...spell, prepared: !spell.prepared } : spell
        ));
    };

    const toggleExpandSpell = (id: string) => {
        setExpandedSpellId(expandedSpellId === id ? null : id);
    };

    const spellSchools = [
        "Abjuration", "Conjuration", "Divination", "Enchantment",
        "Evocation", "Illusion", "Necromancy", "Transmutation"
    ];

    return (
        <div className="space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <Card>
                    <CardBody className="flex flex-col items-center p-3">
                        <div className="flex items-center justify-center w-10 h-10 rounded-full bg-primary-100 dark:bg-primary-900/30 mb-2">
                            <Icon icon="lucide:wand-sparkles" className="w-5 h-5 text-primary-500" />
                        </div>
                        <h3 className="text-sm font-semibold text-slate-700 dark:text-slate-300">Spellcasting Ability</h3>
                        <Select
                            size="sm"
                            selectedKeys={[spellcastingAbility]}
                            onChange={(e) => setSpellcastingAbility(e.target.value)}
                            className="w-20 mt-2"
                        >
                            <SelectItem key="STR">STR</SelectItem>
                            <SelectItem key="DEX">DEX</SelectItem>
                            <SelectItem key="CON">CON</SelectItem>
                            <SelectItem key="INT">INT</SelectItem>
                            <SelectItem key="WIS">WIS</SelectItem>
                            <SelectItem key="CHA">CHA</SelectItem>
                        </Select>
                    </CardBody>
                </Card>
                <Card>
                    <CardBody className="flex flex-col items-center p-3">
                        <div className="flex items-center justify-center w-10 h-10 rounded-full bg-primary-100 dark:bg-primary-900/30 mb-2">
                            <Icon icon="lucide:shield" className="w-5 h-5 text-primary-500" />
                        </div>
                        <h3 className="text-sm font-semibold text-slate-700 dark:text-slate-300">Spell Save DC</h3>
                        <Input
                            type="text"
                            size="sm"
                            value={spellSaveDC}
                            onValueChange={setSpellSaveDC}
                            className="w-16 text-center mt-2"
                        />
                    </CardBody>
                </Card>
                <Card>
                    <CardBody className="flex flex-col items-center p-3">
                        <div className="flex items-center justify-center w-10 h-10 rounded-full bg-primary-100 dark:bg-primary-900/30 mb-2">
                            <Icon icon="lucide:target" className="w-5 h-5 text-primary-500" />
                        </div>
                        <h3 className="text-sm font-semibold text-slate-700 dark:text-slate-300">Spell Attack Bonus</h3>
                        <Input
                            type="text"
                            size="sm"
                            value={spellAttackBonus}
                            onValueChange={setSpellAttackBonus}
                            className="w-16 text-center mt-2"
                        />
                    </CardBody>
                </Card>
            </div>

            <div className="flex justify-between items-center">
                <h2 className="text-xl font-semibold text-slate-800 dark:text-slate-200">Spells</h2>
                <Button
                    color="primary"
                    onPress={() => setShowAddForm(!showAddForm)}
                    startContent={<Icon icon={showAddForm ? "lucide:minus" : "lucide:plus"} />}
                >
                    {showAddForm ? "Cancel" : "Add Spell"}
                </Button>
            </div>

            {showAddForm && (
                <Card>
                    <CardBody className="p-4">
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                            <Input
                                label="Spell Name"
                                placeholder="Enter spell name"
                                value={newSpell.name}
                                onValueChange={(value) => setNewSpell({ ...newSpell, name: value })}
                            />
                            <Select
                                label="Level"
                                placeholder="Select level"
                                selectedKeys={[newSpell.level.toString()]}
                                onChange={(e) => setNewSpell({ ...newSpell, level: parseInt(e.target.value) })}
                            >
                                <SelectItem key="0">Cantrip</SelectItem>
                                <SelectItem key="1">1st Level</SelectItem>
                                <SelectItem key="2">2nd Level</SelectItem>
                                <SelectItem key="3">3rd Level</SelectItem>
                                <SelectItem key="4">4th Level</SelectItem>
                                <SelectItem key="5">5th Level</SelectItem>
                                <SelectItem key="6">6th Level</SelectItem>
                                <SelectItem key="7">7th Level</SelectItem>
                                <SelectItem key="8">8th Level</SelectItem>
                                <SelectItem key="9">9th Level</SelectItem>
                            </Select>
                            <Input
                                label="Casting Time"
                                placeholder="1 Action"
                                value={newSpell.castingTime}
                                onValueChange={(value) => setNewSpell({ ...newSpell, castingTime: value })}
                            />
                            <Input
                                label="Range"
                                placeholder="60 feet"
                                value={newSpell.range}
                                onValueChange={(value) => setNewSpell({ ...newSpell, range: value })}
                            />
                            <Input
                                label="Duration"
                                placeholder="Concentration, up to 1 minute"
                                value={newSpell.duration}
                                onValueChange={(value) => setNewSpell({ ...newSpell, duration: value })}
                            />
                            <Select
                                label="School"
                                placeholder="Select school"
                                selectedKeys={[newSpell.school]}
                                onChange={(e) => setNewSpell({ ...newSpell, school: e.target.value })}
                            >
                                {spellSchools.map(school => (
                                    <SelectItem key={school}>{school}</SelectItem>
                                ))}
                            </Select>
                        </div>
                        <Textarea
                            label="Description"
                            placeholder="Enter spell description"
                            value={newSpell.description}
                            onValueChange={(value) => setNewSpell({ ...newSpell, description: value })}
                            className="mt-4"
                        />
                        <div className="flex justify-end mt-4">
                            <Button
                                color="primary"
                                onPress={handleAddSpell}
                            >
                                Add Spell
                            </Button>
                        </div>
                    </CardBody>
                </Card>
            )}

            <div className="space-y-4">
                {spells.map((spell) => (
                    <Card key={spell.id} className="overflow-hidden">
                        <CardBody className="p-0">
                            <div
                                className="p-4 flex justify-between items-center cursor-pointer"
                                onClick={() => toggleExpandSpell(spell.id)}
                            >
                                <div className="flex items-center gap-3">
                                    <Button
                                        isIconOnly
                                        size="sm"
                                        color={spell.prepared ? "primary" : "default"}
                                        variant={spell.prepared ? "solid" : "bordered"}
                                        onPress={(e) => {
                                            togglePrepared(spell.id);
                                        }}
                                    >
                                        <Icon icon={spell.prepared ? "lucide:check" : "lucide:book"} className="w-4 h-4" />
                                    </Button>
                                    <div>
                                        <h3 className="text-lg font-medium">{spell.name}</h3>
                                        <div className="flex items-center gap-2 text-xs text-slate-500 dark:text-slate-400">
                                            <span>{spell.level === 0 ? "Cantrip" : `Level ${spell.level}`}</span>
                                            <span>-</span>
                                            <span>{spell.school}</span>
                                        </div>
                                    </div>
                                </div>
                                <div className="flex items-center gap-2">
                                    <Button
                                        isIconOnly
                                        size="sm"
                                        color="danger"
                                        variant="light"
                                        onPress={(e) => {
                                            handleRemoveSpell(spell.id);
                                        }}
                                    >
                                        <Icon icon="lucide:trash-2" className="w-4 h-4" />
                                    </Button>
                                    <Icon
                                        icon={expandedSpellId === spell.id ? "lucide:chevron-up" : "lucide:chevron-down"}
                                        className="w-5 h-5 text-slate-400"
                                    />
                                </div>
                            </div>
                            {expandedSpellId === spell.id && (
                                <div className="p-4 pt-0 border-t border-slate-200 dark:border-slate-700">
                                    <div className="grid grid-cols-1 md:grid-cols-3 gap-2 mb-3 text-sm">
                                        <div>
                                            <span className="font-medium text-slate-700 dark:text-slate-300">Casting Time:</span>{" "}
                                            <span className="text-slate-600 dark:text-slate-400">{spell.castingTime}</span>
                                        </div>
                                        <div>
                                            <span className="font-medium text-slate-700 dark:text-slate-300">Range:</span>{" "}
                                            <span className="text-slate-600 dark:text-slate-400">{spell.range}</span>
                                        </div>
                                        <div>
                                            <span className="font-medium text-slate-700 dark:text-slate-300">Duration:</span>{" "}
                                            <span className="text-slate-600 dark:text-slate-400">{spell.duration}</span>
                                        </div>
                                    </div>
                                    <p className="text-slate-600 dark:text-slate-300 text-sm">{spell.description}</p>
                                </div>
                            )}
                        </CardBody>
                    </Card>
                ))}
            </div>
        </div>
    );
};