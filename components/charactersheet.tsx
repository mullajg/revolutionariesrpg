import React from "react";
import { Tabs, Tab, Input, Progress, Textarea, Chip } from "@heroui/react";
import CharacterHeader from "@/components/characterheader";
import AttributesSection from "@/components/attributessection";
import SkillsSection from "@/components/skillssection";
import CombatSection from "@/components/combatsection";
import SpellsSection from "@/components/spellssection";
import InventorySection from "@/components/inventorysection";
import FeaturesSection from "@/components/featuressection";
import NotesSection from "@/components/notessection";

import CompendiumTable from "@/components/compendiumtable";

export default function CharacterSheet(){
    const [selected, setSelected] = React.useState("attributes");
    const [currentHP, setCurrentHP] = React.useState(50);
    const [maxHP, setMaxHP] = React.useState(50);
    const [tempHP, setTempHP] = React.useState(0);

    const handleHPChange = (value: string, type: "current" | "max" | "temp") => {
        const numValue = parseInt(value) || 0;
        if (type === "current") setCurrentHP(numValue);
        if (type === "max") setMaxHP(numValue);
        if (type === "temp") setTempHP(numValue);
    };

    return (
        <div className="flex flex-col min-w-3xl">
            <CharacterHeader />

            <div className="p-4 bg-slate-50 dark:bg-slate-800 border-b border-slate-200 dark:border-slate-700">
                <div className="flex flex-col md:flex-row gap-4 items-center">
                    <div className="flex-1">
                        <h3 className="text-sm font-medium text-slate-500 dark:text-slate-400 mb-1">Hit Points</h3>
                        <div className="flex items-center gap-2">
                            <Input
                                type="number"
                                size="sm"
                                value={currentHP.toString()}
                                onValueChange={(value) => handleHPChange(value, "current")}
                                className="w-16"
                                min={0}
                                max={maxHP}
                            />
                            <span className="text-slate-600 dark:text-slate-300">/</span>
                            <Input
                                type="number"
                                size="sm"
                                value={maxHP.toString()}
                                onValueChange={(value) => handleHPChange(value, "max")}
                                className="w-16"
                                min={1}
                            />
                            <div className="ml-2">
                                <Chip size="sm" color={tempHP > 0 ? "success" : "default"}>
                                    Temp: {tempHP}
                                </Chip>
                            </div>
                        </div>
                        <Progress
                            value={currentHP}
                            maxValue={maxHP}
                            color={currentHP < maxHP / 4 ? "danger" : currentHP < maxHP / 2 ? "warning" : "success"}
                            className="mt-2"
                            aria-label="HP"
                        />
                    </div>
                    <div className="flex gap-2">
                        <Input
                            type="number"
                            size="sm"
                            label="Temp HP"
                            value={tempHP.toString()}
                            onValueChange={(value) => handleHPChange(value, "temp")}
                            className="w-24"
                            min={0}
                        />
                    </div>
                </div>
            </div>

            <Tabs
                aria-label="Character sections"
                className="w-full"
                color="primary"
                variant="underlined"
            >
                <Tab key="attributes" title="Attributes">
                    <div className="p-4">
                        <AttributesSection />
                    </div>
                </Tab>
                <Tab key="skills" title="Skills">
                    <div className="p-4">
                        <SkillsSection />
                    </div>
                </Tab>
                <Tab key="combat" title="Combat">
                    <div className="p-4">
                        <CombatSection />
                    </div>
                </Tab>
                <Tab key="spells" title="Spells">
                    <div className="p-4">
                        <SpellsSection />
                    </div>
                </Tab>
                <Tab key="inventory" title="Inventory">
                    <div className="p-4">
                        <InventorySection />
                    </div>
                </Tab>
                <Tab key="features" title="Features & Feats">
                    <div className="p-4">
                        <FeaturesSection />
                    </div>
                </Tab>
                <Tab key="notes" title="Notes">
                    <div className="p-4">
                        <NotesSection />
                    </div>
                </Tab>
            </Tabs>
        </div>
    );
}